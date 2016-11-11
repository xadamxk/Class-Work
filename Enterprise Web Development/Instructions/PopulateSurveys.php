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
		echo "$sqlString";
		mysqli_query($dbConnection, $sqlString);
		mysqli_select_db($dbConnection, $database);
		$createTable = TRUE;
	}
	else
	{
		echo "<p>Database -", "$database", "- found.</p>";
		mysqli_select_db($dbConnection, $database);
				
		// Geico Commercials
		
		$tableName = "surveys";
		
		$surveyCode = "GEI001";
		$surveyFamily = "Geico";
		$surveyDescription = "Geico Commercials";
		$surveyResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$surveyFamily', '$surveyDescription', $surveyResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($dbConnection)) . "</p>";
		
		$tableName = "results";
		
		$surveyCode = "GEI001";
		$resultCode = "GC01";
		$resultDescription = "Charlie Daniels";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($DBConnection)) . "</p>";
		
		$surveyCode = "GEI001";
		$resultCode = "GC02";
		$resultDescription = "Abraham Lincoln";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($dbConnection)) . "</p>";
	
		$surveyCode = "GEI001";
		$resultCode = "GC03";
		$resultDescription = "Former Drill Sergent";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($DBConnection)) . "</p>";
	
		$surveyCode = "GEI001";
		$resultCode = "GC04";
		$resultDescription = "Woodchucks";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($DBConnection)) . "</p>";
		
		// Budweiser Commercials
		
		$tableName = "surveys";
		
		$surveyCode = "BUD001";
		$surveyFamily = "Budweiser";
		$surveyDescription = "Budweiser Commercials";
		$surveyResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$surveyFamily', '$surveyDescription', $surveyResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($dbConnection)) . "</p>";
		
		$tableName = "results";
		
		$surveyCode = "BUD001";
		$resultCode = "BD01";
		$resultDescription = "Clydesdales";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($DBConnection)) . "</p>";
		
		$surveyCode = "BUD001";
		$resultCode = "BD02";
		$resultDescription = "Frogs";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($dbConnection)) . "</p>";
	
		$surveyCode = "BUD001";
		$resultCode = "BD03";
		$resultDescription = "Real Men of Genius";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($DBConnection)) . "</p>";
	
		$surveyCode = "BUD001";
		$resultCode = "BD04";
		$resultDescription = "Spuds McKenzie";
		$resultResponses = 0;
		
		$SQLstring = "INSERT INTO $tableName VALUES('$surveyCode', '$resultCode', '$resultDescription', $resultResponses)";
		echo $SQLstring."<br />";
		$QueryResult = @mysqli_query($dbConnection, $SQLstring)
		Or die("<p>Unable to execute the query.</p>"
		. "<p>Error code " . mysqli_errno($dbConnection)
		. ": " . mysqli_error($DBConnection)) . "</p>";
	}	
?>