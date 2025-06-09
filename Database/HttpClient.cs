using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum RequestType
{
    GET,
    POST,
    PUT,
    DELETE
}

public struct Result<TResultType>
{
    public bool IsSuccess => ErrorMessage == default;
    public long ResultCode;
    public TResultType Value;
    public string ErrorMessage;
}

public class HttpClient : SingletonMonoBehaviour<HttpClient>
{
    [SerializeField] private bool _devServer;
    
    
    public event Action OnUserChanged;

    private readonly string _productionServer = "https://api.brno.tastyair.cz/";
    private readonly string _developmentServer = "https://api-dev.brno.tastyair.cz/";
    private readonly string _devVersion = "Settings/AppVersion";
    private readonly string _prodVersion = "Settings/AppVersion";

    private string ServerURL = "";
    
    private ISerializationOption _serializationOption;

    public bool IsPlayer(ulong id) => _playerId == id;
    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(_jwtToken);

    private string _proposedNickname = null;
    private ulong _playerId;
    private string _jwtToken = null;
    private string _refreshToken = null;
    private string _nickname = null;

    private string Token => $"Bearer {_jwtToken}";

    private void Awake()
    {
        ServerURL = _devServer ? _developmentServer : _productionServer;
        _serializationOption = new JsonSerializationOption();
    }

    #region Login

    public void Register(string email, Action<HttpResponse> callback)
    {
        /* , string nickname
        GetNicknameResponse nickRequest = new GetNicknameResponse
        {
            nickname = nickname
        };

        // Set nickname before registration 
        // Possible problem ( nickname changed before email confirmation 
        Request<EmptyResponse>(RequestType.PUT, "Players/NicknameSet", nickRequest, (response) => { });
        */
        
        
        RegisterRequest request = new RegisterRequest
        {
            eMailAddress = email
        };
        
        
        Request<RegisterResponse>(RequestType.PUT, "PlayBrnoID/Register", request, (response) => {
            if (!response.Value.success)
            {
                Debug.Log(response.Value.errorText);
                callback?.Invoke(new HttpResponse
                {
                    Result = response.Value.success,
                    ErrorMessage = response.Value.errorText
                });
                return;
            }
            
            callback?.Invoke(new HttpResponse
            {
                Result = response.Value.success,
                ErrorMessage = response.Value.errorText
            });
        });
    }
    public void DeleteAccount(Action<bool> callback)
    {
            EmptyResponse request = new EmptyResponse()
            {
               
            };

            Request<EmptyResponse>(RequestType.DELETE, "Players", request, (response) => {
                if (!response.IsSuccess)
                {
                    callback?.Invoke(false);
                    Debug.Log("false");
                    return;
                }
                callback?.Invoke(true);
                Debug.Log("User is deleted");
            });
        
    }
    public void ResetPassword(string email, Action<HttpResponse> callback)
    {
        ResetPasswordRequest request = new ResetPasswordRequest
        {
            eMailAddress = email
        };
        
        Request<ResetPasswordResponse>(RequestType.PUT, "PlayBrnoID/ResetPassword", request, (response) => {
            if (!response.Value.success)
            {
                Debug.Log(response.Value.errorText);
                callback?.Invoke(new HttpResponse
                {
                    Result = response.Value.success,
                    ErrorMessage = response.Value.errorText
                });
                return;
            }
            
            callback?.Invoke(new HttpResponse
            {
                Result = response.Value.success,
                ErrorMessage = response.Value.errorText
            });
        });
    }

    public void ChangePasswordOld(string email, string oldPassword, string password, Action<HttpResponse> callback)
    {
        ChangePasswordRequest request = new ChangePasswordRequest
        {
            eMailAddress = email,
            resetPasswordCode = null,
            oldPassword = oldPassword,
            newPassword = password
        };
        
        Request<ChangePasswordResponse>(RequestType.PUT, "PlayBrnoID/ChangePassword", request, (response) => {
            if (!response.Value.success)
            {
                Debug.Log(response.Value.errorText);
                callback?.Invoke(new HttpResponse
                {
                    Result = response.Value.success,
                    ErrorMessage = response.Value.errorText
                });
                return;
            }
            
            callback?.Invoke(new HttpResponse
            {
                Result = response.Value.success,
                ErrorMessage = response.Value.errorText
            });
        });
    }
    
    public void ChangePassword(string email, string resetCode, string password, Action<HttpResponse> callback)
    {
        ChangePasswordRequest request = new ChangePasswordRequest
        {
            eMailAddress = email,
            resetPasswordCode = resetCode,
            oldPassword = null,
            newPassword = password
        };
        
        Request<ChangePasswordResponse>(RequestType.PUT, "PlayBrnoID/ChangePassword", request, (response) => {
            if (!response.Value.success)
            {
                Debug.Log(response.Value.errorText);
                callback?.Invoke(new HttpResponse
                {
                    Result = response.Value.success,
                    ErrorMessage = response.Value.errorText
                });
                return;
            }
            
            callback?.Invoke(new HttpResponse
            {
                Result = response.Value.success,
                ErrorMessage = response.Value.errorText
            });
        });
    }
    
    public void Login(Action<bool> callback)
    {
        LoginRequest request = new LoginRequest()
        {
            deviceId = GetDeviceId(),
            facebookId = GetFacebookId(),
            playBrnoId = GetPlayBrnoId(),
            googleSignInToken = GetGoogleId(),
            signWithAppleToken = GetAppleID(),
            proposedNickname = _proposedNickname,
        };
        
        Request<LoginResponse>(RequestType.PUT, "Players/Login", request, (response) => {
            if (!response.IsSuccess)
            {
                callback?.Invoke(false);
                return;
            }

            _jwtToken = response.Value.jwtToken;
            Debug.Log(_jwtToken);
            _refreshToken = response.Value.refreshToken;
            _playerId = response.Value.id;
            _nickname = response.Value.nickname;
            
            callback?.Invoke(true);
            OnUserChanged?.Invoke();
        });
    }
    
    public void LoginPlayBrno(string email, string password, Action<HttpResponse> callback)
    {
        PlayBrnoLoginRequest request = new PlayBrnoLoginRequest()
        {
            eMailAddress = email,
            password = password
        };
        
        Request<PlayBrnoLoginResponse>(RequestType.PUT, "PlayBrnoID/Login", request, (response) => {
            if (!response.Value.success)
            {
                Debug.Log(response.Value.errorText);
                callback?.Invoke(new HttpResponse
                {
                    Result = response.Value.success,
                    ErrorMessage = response.Value.errorText
                });
                return;
            }
            
            SetPlayBrnoId(response.Value.loginSecret);
            
            callback?.Invoke(new HttpResponse
            {
                Result = response.Value.success,
                ErrorMessage = response.Value.errorText
            });
            
            OnUserChanged?.Invoke();
        });
    }
    
    public IEnumerator TryRefreshTokenCoroutine()
    {
        RefreshTokenRequest request = new RefreshTokenRequest()
        {
            jwtTokenOld = _jwtToken,
            refreshToken = _refreshToken
        };
        yield return IE_Request<RefreshTokenResponse>(RequestType.PUT, "Players/RefreshToken", request, (response) =>
        {
            if (response.IsSuccess)
            {
                _jwtToken = response.Value.jwtToken;
            }
            else
            {
                _jwtToken = null;
            }
        });
    }

    public void Logout()
    {
        _jwtToken = null;
        _nickname = null;

        PlayerPrefs.SetString("registration_email", "");
        
        ResetFacebookId();
        ResetPlayBrnoId();
        ResetGoogleId();
        ResetAppleID();
        
        Login((res)=>{OnUserChanged?.Invoke();});
    }
    public void DeleteAndRelogin()
    {
        ResetFacebookId();
        ResetPlayBrnoId();
        ResetGoogleId();
        ResetAppleID();


        LoginHelper.Instance.Login();
    }

    #endregion

    #region Points

    public struct CallBackReason
    {
        public bool Response;
        public int ActualCoins;
    }
    public struct CallBackVersion
    {
        public bool Response;
        public string Version;
    }
    public void AddCoins(int coins, string addComment, Action<CallBackReason> callback)
    {
        CoinsIncreaseRequest request = new CoinsIncreaseRequest()
        {
            coinsIncrease = coins,
            comment = addComment
        };

        Request<CoinsResponse>(RequestType.PUT, "Players/CoinsIncrease", request, (response) =>
        {
            callback?.Invoke(new CallBackReason
            {
                Response = response.IsSuccess,
                ActualCoins = response.Value.coinCount
            });
        });
    }
    
    public void RemoveCoins(int coins, string removeComment, Action<CallBackReason> callback)
    {
        CoinsDecreaseRequest request = new CoinsDecreaseRequest()
        {
            coinsDecrease = coins,
            comment = removeComment
        };

        Request<CoinsResponse>(RequestType.PUT, "Players/CoinsDecrease", request, (response) =>
        {
            callback?.Invoke(new CallBackReason
            {
                Response = response.IsSuccess,
                ActualCoins = response.Value.coinCount
            });
        });
    }
    
    public void GetCoins(Action<CallBackReason> callback)
    {
        EmptyResponse request = new EmptyResponse();

        Request<CoinsResponse>(RequestType.GET, "Players/CoinsGet", request, (response) =>
        {

            callback.Invoke(new CallBackReason
            {
                Response = response.IsSuccess,
                ActualCoins = response.IsSuccess ? response.Value.coinCount : -1
            });
        });
    }
    public void GetVersion(Action<CallBackVersion> callback)
    {
        EmptyResponse request = new EmptyResponse();

        Request<string>(RequestType.GET, "Settings/AppVersion", request, (response) =>
        {
            /*
            callback.Invoke(new CallBackVersion
            {
                Response = response.IsSuccess,
                Version = response.Value.key
            });*/
        });
    }

    public void GetLeaderboard(Action<List<LeaderboardRecordData>> callback)
    {
        EmptyResponse request = new EmptyResponse();

        Request<LeaderboardResponse>(RequestType.GET, "Players/GetCoinsLeaderboard", request, (response) =>
        {
            if (!response.IsSuccess)
            {
                callback?.Invoke(null);
                return;
            }
            
            callback.Invoke(response.Value.lines);
        });
    }
    
    #endregion
    
    #region Request Functions

    public void GetNickname(Action<string> callback)
    {
        Request<GetNicknameResponse>(RequestType.GET, "Players/NicknameGet", null, (response) => {
            if (!response.IsSuccess)
            {
                callback?.Invoke(null);
                return;
            }

            callback?.Invoke(response.Value.nickname);
        });
    }

    public void SetNickname(string nickname)
    {
        GetNicknameResponse nickRequest = new GetNicknameResponse
        {
            nickname = nickname
        };
        
        Request<EmptyResponse>(RequestType.PUT, "Players/NicknameSet", nickRequest, (response) => { });
    }

    public void SetNickname(string nickname, Action<HttpResponse> callback)
    {
        GetNicknameResponse nickRequest = new GetNicknameResponse
        {
            nickname = nickname
        };
        
        Request<SetNicknameResponse>(RequestType.PUT, "Players/NicknameSet", nickRequest, (response) =>
        {
            callback?.Invoke(new HttpResponse
            {
                Result = response.Value.successful,
                ErrorMessage = response.Value.errorText
            });
            
            /*
            if(response.IsSuccess) OnUserChanged?.Invoke();
            callback?.Invoke(response.IsSuccess);*/
        });
    }
    
    
    #endregion
    
    #region Utils

    // DEVICE ID - Anonymous
    private string GetDeviceId() {
        if (!PlayerPrefs.HasKey("LoginAPI_DeviceID"))
        {
            PlayerPrefs.SetString("LoginAPI_DeviceID", Guid.NewGuid().ToString());
        }
        Debug.Log(PlayerPrefs.GetString("LoginAPI_DeviceID"));
        return PlayerPrefs.GetString("LoginAPI_DeviceID");
    }
    
    // PLAY BRNO
    public void SetPlayBrnoId(string playbrnoId, string nickname = null)
    {
        PlayerPrefs.SetString("LoginAPI_PlayBrnoID",playbrnoId);
        if (nickname != null)
        {
            _proposedNickname = nickname;
        }
    }
    
    public string GetPlayBrnoId()
    {
        if (!PlayerPrefs.HasKey("LoginAPI_PlayBrnoID"))
        {
            return null;
        }
        return PlayerPrefs.GetString("LoginAPI_PlayBrnoID");
    }
    
    public void ResetPlayBrnoId()
    {
        PlayerPrefs.DeleteKey("LoginAPI_PlayBrnoID");
    }
    
    // Apple
    public void SetAppleID(string appleId, string nickname = null)
    {
        PlayerPrefs.SetString("LoginAPI_AppleID",appleId);
        if (nickname != null)
        {
            _proposedNickname = nickname;
        }
    }

    public string GetAppleID()
    {
        if (!PlayerPrefs.HasKey("LoginAPI_AppleID"))
        {
            return null;
        }
        return PlayerPrefs.GetString("LoginAPI_AppleID");
    }

    public void ResetAppleID()
    {
        PlayerPrefs.DeleteKey("LoginAPI_AppleID");
    }
    
    // GOOGLE
    public void SetGoogleId(string googleId, string nickname = null)
    {
        PlayerPrefs.SetString("LoginAPI_GoogleID",googleId);
        if (nickname != null)
        {
            _proposedNickname = nickname;
        }
    }

    public string GetGoogleId()
    {
        if (!PlayerPrefs.HasKey("LoginAPI_GoogleID"))
        {
            return null;
        }
        return PlayerPrefs.GetString("LoginAPI_GoogleID");
    }

    public void ResetGoogleId()
    {
        PlayerPrefs.DeleteKey("LoginAPI_GoogleID");
    }
    
    // FACEBOOK
    
    public void SetFacebookId(string facebookId, string nickname = null)
    {
        PlayerPrefs.SetString("LoginAPI_FacebookID", facebookId);
        if (nickname != null)
        {
            _proposedNickname = nickname;
        }
    }

    public string GetFacebookId()
    {
        if (!PlayerPrefs.HasKey("LoginAPI_FacebookID"))
        {
            return null;
        }
        return PlayerPrefs.GetString("LoginAPI_FacebookID");
    }

    public void ResetFacebookId()
    {
        PlayerPrefs.DeleteKey("LoginAPI_FacebookID");
    }
    
    #endregion
    
    #region Request
    
    private UnityWebRequest WebRequest(RequestType requestType, string endpoint, object data) =>
        requestType switch
        {
            RequestType.GET => UnityWebRequest.Get(ServerURL + endpoint),
            RequestType.PUT => UnityWebRequest.Put(ServerURL + endpoint, _serializationOption.Serialize(data)),
            RequestType.POST => UnityWebRequest.Post(ServerURL + endpoint, _serializationOption.Serialize(data)),
            RequestType.DELETE => UnityWebRequest.Delete(ServerURL + endpoint),
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), $"Invalid Request Type: {requestType}")

        };


    public void Request<TResultType>(RequestType requestType, string endpoint, object data,
        Action<Result<TResultType>> callback)
    {
        StartCoroutine(IE_Request(requestType, endpoint, data, callback));
    }

    private IEnumerator IE_Request<TResultType>(RequestType requestType, string endpoint, object data,
        Action<Result<TResultType>> callback)
    {
        UnityWebRequest www = WebRequest(requestType, endpoint, data);

        Debug.Log(www.url);
        
        // TODO 
        // Ask why is login PUT with POST method
        if (requestType == RequestType.PUT) www.method = "POST";
        
        using (www)
        {
            www.SetRequestHeader("Accept", "application/json; charset=UTF-8");
            www.SetRequestHeader("Content-Type", _serializationOption.ContentType);                
            if (_jwtToken != null)
            {
                www.SetRequestHeader("Authorization", Token);
            }

            yield return www.SendWebRequest();

            
            if (www.result != UnityWebRequest.Result.Success) // Tady přidat kontrolu ne na success ale na result success
            {
                Debug.LogError($"UnityWebRequest error:\n{www.result} - {www.responseCode}");
                /*
                 callback?.Invoke(new Result<TResultType> {
                     ResultCode = www.responseCode,
                     Value = default(TResultType),
                     ErrorMessage = www.result.ToString()
                 });
                 yield break;
                 */
            }
            
            // TODO: process unauthenticated - relogin (using refreshToken)
            if (www.responseCode == 401)
            {
                Debug.Log("unauthorized, trying to refresh token");
                yield return TryRefreshTokenCoroutine();
                if (_jwtToken != null)
                {
                    yield return IE_Request(requestType, endpoint, data, callback);
                    yield break;
                }
            }
            
            if (www.responseCode != 200)
            {
                Debug.LogError($"HTTP error: {www.responseCode}\n{www.error}");
                callback?.Invoke(new Result<TResultType> {
                    ResultCode = www.responseCode,
                    Value = default(TResultType),
                    ErrorMessage = www.error
                });
                yield break;
            }

            TResultType result = default;

            if (requestType == RequestType.DELETE)
            {
                callback?.Invoke(new Result<TResultType>
                {
                    ResultCode = www.responseCode,
                    Value = result,
                    ErrorMessage = default
                });
                yield break;
            }

            try
            {
                
                result = JsonUtility.FromJson<TResultType>(www.downloadHandler.text);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error parsing response:\n{e}");
                callback?.Invoke(new Result<TResultType> {
                    ResultCode = www.responseCode,
                    Value = result,
                    ErrorMessage = e.ToString()
                });
                yield break;
            }

            callback?.Invoke(new Result<TResultType> {
                ResultCode = www.responseCode,
                Value = result,
                ErrorMessage = default
            });
        }
        
    }

    #endregion
    
    #region Async/Await
    /*
    public static async Task<TResultType> Request<TResultType>(RequestType requestType, string endpoint, object data,
       Action<Result<TResultType>> callback)
    {
       UnityWebRequest www = WebRequest(requestType, endpoint, data);

       using (www)
       {
           www.SetRequestHeader("Accept", "application/json; charset=UTF-8");
           www.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
           if (_jwtToken != null)
           {
               www.SetRequestHeader("Authorization", $"Bearer {_jwtToken}");
           }
           
           var operation = www.SendWebRequest();

           while (!operation.isDone)
               await Task.Yield();

           if (www.result != UnityWebRequest.Result.Success)
           {
               
           }
       }
    };
   */
    #endregion
}
