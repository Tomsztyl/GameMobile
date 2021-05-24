<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "id14301516_unitymobile";



if (isset($_POST["loginUser"]) && isset($_POST["loginPass"]))
{

//variables submited by user
$loginUser=$_POST["loginUser"];
$loginPass=$_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
 
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT password FROM users WHERE username='" .$loginUser."' OR email='" .$loginUser."'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if ($row["password"]==$loginPass)
    {
        $sql = "SELECT score FROM users WHERE username='" .$loginUser."' OR email='" .$loginUser."'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0)
        {
            // output data of each row
            while($row = $result->fetch_assoc())
            {
                echo $row["score"];
            }
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