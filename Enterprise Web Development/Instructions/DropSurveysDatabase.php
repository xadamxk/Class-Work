<?php
	
	$dbHost = "localhost";
	$dbUser = "enolan";
	$dbPassword = "erndog";

	$dbConnection = mysqli_connect($dbHost, $dbUser, $dbPassword) or die ('Error connecting to mySQL');	

	$database = "surveys";
	mysqli_select_db($dbConnection, $database);
	
	if (!mysqli_select_db($dbConnection, $database))
	{
		echo "<p>Database -", "$database", "- not found.<br />";
	}
	else
	{
		echo "<p>Database -", "$database", "- found.<br />";
		$sqlString = "DROP DATABASE $database";
		ECHO "$sqlString";
		mysqli_query($dbConnection, $sqlString);
		echo "<p>Database -", "$database", "- dropped.<br />";
	}	

?>