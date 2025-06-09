<?php
$conn = new mysqli("sql306.infinityfree.com", "if0_39132522", "100020003Tr", "if0_39132522_Checki");
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$conn->close();
?>
