<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "id14301516_unitymobile";

//Variables submitted by user

if (isset($_POST["registerUser"]) && isset($_POST["registerMail"]) && isset($_POST["registerPass"]))
{
  $registerUser=$_POST["registerUser"];
  $registerMail=$_POST["registerMail"];
  $registerPass=$_POST["registerPass"];

//Variable Date
$date = new DateTime('now');
$convertDateNow=$date->format("Y-m-d H:i:s");

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql="SELECT  username FROM users WHERE username='" .$registerUser."' OR email='".$registerMail."'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "It is exists";
} else {
    //echo "Username does not exists.";
    $sql = "INSERT INTO users (username, password,email,datecreateAccount)
    VALUES ('$registerUser','$registerPass','$registerMail','$convertDateNow')";   

    if ($conn->query($sql) === TRUE) {
     echo "New user is create";
    } else {
    echo "Error: " . $sql . "<br>" . $conn->error;
    }
}
$conn->close();
}
?>