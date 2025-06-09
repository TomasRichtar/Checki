<?php
$conn = new mysqli("sql306.infinityfree.com", "if0_39132522", "100020003Tr", "if0_39132522_Checki");
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$profileID = $_GET['ProfileID'] ?? '';

$stmt = $conn->prepare("SELECT * FROM Rewards WHERE ProfileID = ?");
$stmt->bind_param("i", $profileID);
$stmt->execute();

$result = $stmt->get_result();
$data = array();

while ($row = $result->fetch_assoc()) {
    $data[] = $row;
}

echo json_encode($data);

$stmt->close();
$conn->close();
?>
