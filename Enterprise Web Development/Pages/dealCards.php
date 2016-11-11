<?php 
// Vars
$numPlayers = $_REQUEST["q"];
$numCards = 5;
$totCards = $numCards * $numPlayers;
$bool = false;
// Arrays
$cardSuitArray = array("Clubs   ","Diamonds","Hearts  ","Spades  ");
$cardValArray = array("A","2","3","4","5","6","7","8","9","10","J","Q","K");
$playershandArray = new SplFixedArray($totCards);
// Check inputs
	if (intval($numPlayers) > 0 && intval($numPlayers) < 5)
		// Generate Hands/ populate arrays
		for ($x = 0; $x < $numPlayers; $x++)
			for ($y = 0; $y < 5; $y++)
				generateHand($x, $y,$cardValArray,$cardSuitArray,$numPlayers,$playershandArray,$bool);
	else
		echo "Must be 1 - 4 Players!";
// Print
	for($x = 0; $x < $totCards; $x++)
		echo $num = 
			($x == 0 ? "<br />Player:1&nbsp&#9;$playershandArray[$x]&#9;" : 
			($x == 5 ? "<br />Player:2&nbsp&#9;$playershandArray[$x]&#9;" : 
			($x == 10 ? "<br />Player:3&nbsp&#9;$playershandArray[$x]&#9;" : 
			($x == 15 ? "<br />Player:4&nbsp&#9;$playershandArray[$x]&#9;" :
				$playershandArray[$x] . "&#9;"))));
	
function generateHand($x,$y,$cardValArray,$cardSuitArray,$numPlayers,$playershandArray){
	// Variable for errors (true = error)
		$bool = false;
	// Random Card Generator
		$temp = (string)($cardValArray[(string)array_rand($cardValArray,1)] . (string)$cardSuitArray[(string)array_rand($cardSuitArray,1)]);
	// Check list of drawn cards
	for ($count = 0; $count < (5 * $numPlayers);$count++)
		if($temp == $playershandArray[$count]) $bool = true;
	// Found Duplicate
		if ($bool) generateHand($x,$y,$cardValArray,$cardSuitArray,$numPlayers,$playershandArray);
	// Save Card
		else
			$playershandArray[($x*5)+($y)] = $temp;
}
?>

