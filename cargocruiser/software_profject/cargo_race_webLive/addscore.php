<?php 

   $dbname = 'cargorac_cargoracedb';
   $hostname = 'localhost';
   $username = 'cargorac';
   $password = 'snorkle1';
   
   
   
	$conn = new mysqli($hostname, $username, $password, $dbname);
	//Check Connection
	if(!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	$name = $_POST["namePost"];
    $score = $_POST["scorePost"];

	$sql = "INSERT INTO scores (name, score)
			VALUES ('".$name."','".$score."')";
	$result = mysqli_query($conn ,$sql);
	
	if(!result) echo "oops..error";
	else echo "everything ok";
			
 
?>