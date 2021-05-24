<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "id14301516_unitymobile";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username, score FROM users ORDER BY score DESC LIMIT 10";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  $i=1;
 // output data of each row
 while($row = $result->fetch_assoc())
 {
     echo $i.".".$row["username"]. ": [". $row["score"]. "] score \n";
     $i++;
 }
} else {
  echo "Nobody broke any record";
}
$conn->close();
?>