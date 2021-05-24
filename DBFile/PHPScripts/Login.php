<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "id14301516_unitymobile";

//variables submited by user
if (isset($_POST["loginUser"]) && isset($_POST["loginPass"]))
{

  $loginUser=$_POST["loginUser"];
  $loginPass=$_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
 
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//Check login data
$sql = "SELECT password FROM users WHERE username='" .$loginUser."' OR email='" .$loginUser."'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if ($row["password"]==$loginPass)
    {
      echo "Login Success";
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