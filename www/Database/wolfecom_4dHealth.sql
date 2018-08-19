-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Aug 19, 2018 at 06:45 AM
-- Server version: 5.5.51-38.2
-- PHP Version: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `wolfecom_4dHealth`
--

-- --------------------------------------------------------

--
-- Table structure for table `flc_model_data`
--

CREATE TABLE `flc_model_data` (
  `ColorR` int(3) NOT NULL,
  `ColorG` int(3) NOT NULL,
  `ColorB` int(3) NOT NULL,
  `ColorAlpha` int(3) NOT NULL,
  `SpeedX` float NOT NULL,
  `SpeedY` float NOT NULL,
  `SpeedZ` float NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `flc_model_data`
--

INSERT INTO `flc_model_data` (`ColorR`, `ColorG`, `ColorB`, `ColorAlpha`, `SpeedX`, `SpeedY`, `SpeedZ`) VALUES
(231, 111, 255, 255, 300, 20, -10);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
