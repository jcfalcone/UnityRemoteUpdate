<?php

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

$_POST = json_decode(file_get_contents('php://input'), true);

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

$found = false;

while($Values = $Sql->fetch(PDO::FETCH_OBJ))
{
	$json->Rotation[0] = $Values->SpeedX;
	$json->Rotation[1] = $Values->SpeedY;
	$json->Rotation[2] = $Values->SpeedZ;

	$json->Color[0] = $Values->ColorR;
	$json->Color[1] = $Values->ColorG;
	$json->Color[2] = $Values->ColorB;
	$json->Color[3] = $Values->ColorAlpha;

	$found = true;
}

if(isset($_POST['ColorR']))
{
	$json->Color[0] = (int)$_POST['ColorR'];
}

if(isset($_POST['ColorG']))
{
	$json->Color[1] = (int)$_POST['ColorG'];
}

if(isset($_POST['ColorB']))
{
	$json->Color[2] = (int)$_POST['ColorB'];
}

if(isset($_POST['ColorAlpha']))
{
	$json->Color[3] = (int)$_POST['ColorAlpha'];
}

if(isset($_POST['SpeedX']))
{
	$json->Rotation[0] = (float)$_POST['SpeedX'];
}

if(isset($_POST['SpeedY']))
{
	$json->Rotation[1] = (float)$_POST['SpeedY'];
}

if(isset($_POST['SpeedZ']))
{
	$json->Rotation[2] = (float)$_POST['SpeedZ'];
}

if($found)
{

	$Sql = 'Update flc_model_data Fmd Set ColorR = :ColorR,
										  ColorG = :ColorG,
										  ColorB = :ColorB,
										  ColorAlpha = :ColorAlpha,
										  SpeedX = :SpeedX,
										  SpeedY = :SpeedY,
									      SpeedZ = :SpeedZ';
}
else
{

	$Sql = 'Insert Into flc_model_data(ColorR,
									   ColorG,
									   ColorB,
									   ColorAlpha,
									   SpeedX,
									   SpeedY,
								       SpeedZ)
							    Values(:ColorR,
									   :ColorG,
									   :ColorB,
									   :ColorAlpha,
									   :SpeedX,
									   :SpeedY,
									   :SpeedZ)';
}

$Sql = $PDO->prepare($Sql);

$Sql->bindValue(':ColorR', 	   $json->Color[0], PDO::PARAM_INT);
$Sql->bindValue(':ColorG', 	   $json->Color[1], PDO::PARAM_INT);
$Sql->bindValue(':ColorB', 	   $json->Color[2], PDO::PARAM_INT);
$Sql->bindValue(':ColorAlpha', $json->Color[3], PDO::PARAM_INT);
$Sql->bindValue(':SpeedX', $json->Rotation[0], PDO::PARAM_STR);
$Sql->bindValue(':SpeedY', $json->Rotation[1], PDO::PARAM_STR);
$Sql->bindValue(':SpeedZ', $json->Rotation[2], PDO::PARAM_STR);

$Sql->execute();

echo json_encode($json);

?>