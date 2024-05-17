CREATE DATABASE  IF NOT EXISTS `bookingDatabase` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci */;
USE `bookingDatabase`;
-- MySQL dump 10.13  Distrib 8.0.36, for macos14 (x86_64)
--
-- Host: 127.0.0.1    Database: bookingDatabase
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.28-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `drzave`
--

DROP TABLE IF EXISTS `drzave`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drzave` (
  `drzava_id` int(11) NOT NULL,
  `ime` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`drzava_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drzave`
--

LOCK TABLES `drzave` WRITE;
/*!40000 ALTER TABLE `drzave` DISABLE KEYS */;
INSERT INTO `drzave` VALUES (1,'test');
/*!40000 ALTER TABLE `drzave` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `listingi`
--

DROP TABLE IF EXISTS `listingi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `listingi` (
  `listing_id` int(11) NOT NULL,
  `datum_od` date DEFAULT NULL,
  `datum_do` date DEFAULT NULL,
  `neprimicnina_id` int(11) DEFAULT NULL,
  `uporabnik_id` int(11) DEFAULT NULL,
  `opis` longtext DEFAULT NULL,
  `slika_url` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`listing_id`),
  KEY `uporabnik_id` (`uporabnik_id`),
  KEY `neprimicnina_id` (`neprimicnina_id`),
  CONSTRAINT `listingi_ibfk_1` FOREIGN KEY (`uporabnik_id`) REFERENCES `uporabniki` (`uporabnik_id`),
  CONSTRAINT `listingi_ibfk_2` FOREIGN KEY (`neprimicnina_id`) REFERENCES `neprimicnine` (`nepremicnina_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `listingi`
--

LOCK TABLES `listingi` WRITE;
/*!40000 ALTER TABLE `listingi` DISABLE KEYS */;
INSERT INTO `listingi` VALUES (1,'2024-01-01','2024-12-30',1,1,'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',NULL),(2,'2024-01-01','2024-11-01',1,1,NULL,NULL),(3,'2024-01-01','2024-11-01',1,1,NULL,NULL),(4,'2024-01-01','2024-11-01',1,1,NULL,NULL);
/*!40000 ALTER TABLE `listingi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `neprimicnine`
--

DROP TABLE IF EXISTS `neprimicnine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `neprimicnine` (
  `nepremicnina_id` int(11) NOT NULL,
  `naslov` varchar(255) DEFAULT NULL,
  `kraj` varchar(255) DEFAULT NULL,
  `postna_st` varchar(4) DEFAULT NULL,
  `uporabnik_id` int(11) DEFAULT NULL,
  `nadstropje` varchar(45) DEFAULT NULL,
  `stevilka_sobe` varchar(45) DEFAULT NULL,
  `drzava_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`nepremicnina_id`),
  KEY `uporabnik_id` (`uporabnik_id`),
  KEY `drzava_id` (`drzava_id`),
  CONSTRAINT `neprimicnine_ibfk_1` FOREIGN KEY (`uporabnik_id`) REFERENCES `uporabniki` (`uporabnik_id`),
  CONSTRAINT `neprimicnine_ibfk_2` FOREIGN KEY (`drzava_id`) REFERENCES `drzave` (`drzava_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `neprimicnine`
--

LOCK TABLES `neprimicnine` WRITE;
/*!40000 ALTER TABLE `neprimicnine` DISABLE KEYS */;
INSERT INTO `neprimicnine` VALUES (1,'Test 10','Ljubljana','1000',1,NULL,NULL,1);
/*!40000 ALTER TABLE `neprimicnine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rezervacije`
--

DROP TABLE IF EXISTS `rezervacije`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rezervacije` (
  `rezervacija_id` int(11) NOT NULL,
  `datum_od` date DEFAULT NULL,
  `datum_do` date DEFAULT NULL,
  `listing_id` int(11) DEFAULT NULL,
  `uporabnik_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`rezervacija_id`),
  KEY `uporabnik_id` (`uporabnik_id`),
  KEY `listing_id` (`listing_id`),
  CONSTRAINT `rezervacije_ibfk_1` FOREIGN KEY (`uporabnik_id`) REFERENCES `uporabniki` (`uporabnik_id`),
  CONSTRAINT `rezervacije_ibfk_2` FOREIGN KEY (`listing_id`) REFERENCES `listingi` (`listing_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rezervacije`
--

LOCK TABLES `rezervacije` WRITE;
/*!40000 ALTER TABLE `rezervacije` DISABLE KEYS */;
/*!40000 ALTER TABLE `rezervacije` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipi_uporabnika`
--

DROP TABLE IF EXISTS `tipi_uporabnika`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipi_uporabnika` (
  `tip_id` int(11) NOT NULL,
  `naziv` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`tip_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipi_uporabnika`
--

LOCK TABLES `tipi_uporabnika` WRITE;
/*!40000 ALTER TABLE `tipi_uporabnika` DISABLE KEYS */;
INSERT INTO `tipi_uporabnika` VALUES (1,'Stranka'),(2,'Lastnik neprimicnine'),(3,'Administrator');
/*!40000 ALTER TABLE `tipi_uporabnika` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uporabniki`
--

DROP TABLE IF EXISTS `uporabniki`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `uporabniki` (
  `uporabnik_id` int(11) NOT NULL,
  `ime` varchar(255) DEFAULT NULL,
  `priimek` varchar(255) DEFAULT NULL,
  `datum_rojstva` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `geslo` varchar(255) DEFAULT NULL,
  `tip_uporabnika_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`uporabnik_id`),
  KEY `tip_uporabnika_id` (`tip_uporabnika_id`),
  CONSTRAINT `uporabniki_ibfk_1` FOREIGN KEY (`tip_uporabnika_id`) REFERENCES `tipi_uporabnika` (`tip_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uporabniki`
--

LOCK TABLES `uporabniki` WRITE;
/*!40000 ALTER TABLE `uporabniki` DISABLE KEYS */;
INSERT INTO `uporabniki` VALUES (1,'test','test','1-1-2000','test@test.si','root',2);
/*!40000 ALTER TABLE `uporabniki` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-17  3:51:23
