<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "id14301516_unitymobile";



if (isset($_POST["loginUser"]) && isset($_POST["loginPass"]) && isset($_POST["bestScore"]))
{

//variables submited by user
$loginUser=$_POST["loginUser"];
$loginPass=$_POST["loginPass"];

//variables bestscore
$bestScore=$_POST["bestScore"];


// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
 
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}


//Check GoodLogin

$sql = "SELECT password FROM users WHERE username='" .$loginUser."' OR email='" .$loginUser."'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if ($row["password"]==$loginPass)
    {
      $sql = "UPDATE users SET score='" .$bestScore."' WHERE username='" .$loginUser."' OR email='" .$loginUser."'";

      if ($conn->query($sql) === TRUE) {
        echo "Record updated successfully";
      } else {
        echo "Error updating record: " . $conn->error;
      }
    }
    else
    {
        echo "Wrong Password";
    }
  }
} else {
  echo "Username does not exists.";
}

$conn->close();
}
?>