<?php
	
    $myscores = $_POST['scores'];

   //echo "Started Executing <br />";
   $dbname = 'cargorac_cargoracedb';
   $hostname = 'localhost';
   $username = 'cargorac';
   $password = 'snorkle1';
   
	@ $db = new mysqli("$hostname", "$username", "$password", "$dbname");
	if (mysqli_connect_errno()) {
	echo 'Error: Could not connect to database.  Please try again later.';
	exit;
	}

    try {
        $dbh = new PDO('mysql:host='. $hostname .';dbname='. $dbname, $username, $password);
    } catch(PDOException $e) {
        echo '<h1>An error has occurred.</h1><pre>', $e->getMessage() ,'</pre>';
    }
 
    $sth = $dbh->query('SELECT * FROM scores ORDER BY score DESC');
    $sth->setFetchMode(PDO::FETCH_ASSOC);
 
    $result = $sth->fetchAll();
 
    if(count($result) > 0) {
        foreach($result as $r) {
            echo $r['name'], ":\t", $r['score'], "<br/><br/><br/>";
        }
    }

?>