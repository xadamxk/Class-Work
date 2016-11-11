<?php
	
	$dbHost = "localhost";
	$dbUser = "enolan";
	$dbPassword = "erndog";

	$dbConnection = mysqli_connect($dbHost, $dbUser, $dbPassword) or die ('Error connecting to mySQL');	

	$database = "surveys";
	mysqli_select_db($dbConnection, $database);
	
	$createTable = FALSE;

	if (!mysqli_select_db($dbConnection, $database))
	{
		echo "<p>Database -", "$database", "- not found.</p>";
		$sqlString = "CREATE DATABASE $database";
		echo "$sqlString"."<br />";
		mysqli_query($dbConnection, $sqlString);
		mysqli_select_db($dbConnection, $database);
		$createTable = TRUE;
	}
	else
	{
		echo "<p>Database -", "$database", "- found.</p>";
		mysqli_select_db($dbConnection, $database);
	}	
			
	if ($createTable)
	{
		$table = "participants";
		$create = "CREATE TABLE $table 
		(participantUserName VARCHAR(12),
		participantPassword VARCHAR(12),
		participantLastname VARCHAR(24),
		participantFirstName VARCHAR(24),
		participantMiddleName VARCHAR(24),
		participantAddress1 VARCHAR(26),
		participantAddress2 VARCHAR(26),
		participantCity VARCHAR(26),
		participantState VARCHAR(12),
		participantZipCode VARCHAR(10),
		PRIMARY KEY (participantUserName))";
		echo $create;
		mysqli_query($dbConnection, $create);
		echo "<p>Successfully created the -", "$table", "- table</p>";
		
		$table = "participation";
		$create = "CREATE TABLE $table 
		(participationKey SMALLINT(6) UNSIGNED NOT NULL AUTO_INCREMENT,
		participationUserName VARCHAR(12),
		participationPassword VARCHAR(12),
		participationSurveyCode VARCHAR(6),
		participationDate VARCHAR(13),
		PRIMARY KEY (participationKey))";
		echo $create;
		mysqli_query($dbConnection, $create);
		echo "<p>Successfully created the -", "$table", "- table</p>";
		
		$table = "surveys";
		$create = "CREATE TABLE $table 
		(surveyCode VARCHAR(6),
		surveyFamily VARCHAR(12),
		surveyDescription VARCHAR(20),
		surveyResponses INT(7),
		PRIMARY KEY (surveyCode))";
		echo $create;
		mysqli_query($dbConnection, $create);
		echo "<p>Successfully created the -", "$table", "- table</p>";
		
		$table = "results";
		$create = "CREATE TABLE $table
		(surveyCode VARCHAR(6),
		resultCode VARCHAR(6),
		resultDescription VARCHAR(20),
		resultResponses INT(7),
		PRIMARY KEY (resultCode))";
		echo $create;
		mysqli_query($dbConnection, $create);
		echo "<p>Successfully created the -", "$table", "- table</p>";
	}
?>