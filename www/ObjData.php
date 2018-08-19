<?php

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);


$PDO = new PDO('mysql:host=localhost;dbname=wolfecom_4dHealth', 'wolfecom_temp', 'temp0078');


$Sql = 'Select ColorR,
			   ColorG,
			   ColorB,
			   ColorAlpha,
			   SpeedX,
			   SpeedY,
			   SpeedZ
		  From flc_model_data Fmd
		 Limit 0, 1';

$Sql = $PDO->prepare($Sql);
$Sql->execute();

$json = new stdClass();

$json->Rotation = array();
$json->Rotation[] = 0.0;
$json->Rotation[] = 0.0;
$json->Rotation[] = 0.0;

$json->Color = array();
$json->Color[] = 255.0;
$json->Color[] = 255.0;
$json->Color[] = 255.0;
$json->Color[] = 255.0;

while($Values = $Sql->fetch(PDO::FETCH_OBJ))
{
	$json->Rotation[0] = $Values->SpeedX;
	$json->Rotation[1] = $Values->SpeedY;
	$json->Rotation[2] = $Values->SpeedZ;

	$json->Color[0] = $Values->ColorR;
	$json->Color[1] = $Values->ColorG;
	$json->Color[2] = $Values->ColorB;
	$json->Color[3] = $Values->ColorAlpha;
}

echo json_encode($json);

?>