-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: cs633project
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `events`
--

DROP TABLE IF EXISTS `events`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `events` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(100) NOT NULL,
  `tags` varchar(100) DEFAULT NULL,
  `tagid` varchar(100) DEFAULT NULL,
  `author` varchar(100) DEFAULT NULL,
  `event` varchar(10000) NOT NULL,
  `date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
INSERT INTO `events` VALUES (1,'Test','test,project','21,20,','jack','TEST','2022-09-17 12:53:56'),(2,'Test','test,project','21,20,','jack','TEST','2022-09-17 12:55:11');
/*!40000 ALTER TABLE `events` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loginstatus`
--

DROP TABLE IF EXISTS `loginstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `loginstatus` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(100) NOT NULL,
  `status` tinyint NOT NULL,
  `last_login_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loginstatus`
--

LOCK TABLES `loginstatus` WRITE;
/*!40000 ALTER TABLE `loginstatus` DISABLE KEYS */;
INSERT INTO `loginstatus` VALUES (1,'jack',0,'2022-09-17 18:09:40'),(2,'Tyron',0,'2022-09-17 18:12:21');
/*!40000 ALTER TABLE `loginstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `messagebox`
--

DROP TABLE IF EXISTS `messagebox`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `messagebox` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `from_user` varchar(100) NOT NULL,
  `to_user` varchar(100) NOT NULL,
  `message` varchar(1000) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `messagebox`
--

LOCK TABLES `messagebox` WRITE;
/*!40000 ALTER TABLE `messagebox` DISABLE KEYS */;
INSERT INTO `messagebox` VALUES (2,'jack','Tyron','Hi, this is jack.','2022-09-16 23:58:08'),(3,'jack','Tyron','test1','2022-09-17 00:40:55'),(4,'jack','Tyron','test2','2022-09-17 00:40:59'),(5,'jack','Tyron','test3','2022-09-17 00:41:01'),(6,'jack','Tyron','test4','2022-09-17 00:41:03'),(7,'jack','Tyron','Hi, test.','2022-09-17 11:11:16');
/*!40000 ALTER TABLE `messagebox` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tag`
--

DROP TABLE IF EXISTS `tag`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tag` (
  `tagid` int unsigned NOT NULL AUTO_INCREMENT,
  `tagname` varchar(100) NOT NULL,
  `queto_num` int unsigned NOT NULL,
  PRIMARY KEY (`tagid`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tag`
--

LOCK TABLES `tag` WRITE;
/*!40000 ALTER TABLE `tag` DISABLE KEYS */;
INSERT INTO `tag` VALUES (17,'computer',3),(18,'cs',3),(19,'course',3),(20,'project',6),(21,'test',3);
/*!40000 ALTER TABLE `tag` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userinfo`
--

DROP TABLE IF EXISTS `userinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userinfo` (
  `userid` int unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(100) NOT NULL,
  `password` varchar(50) NOT NULL,
  `useremail` varchar(100) NOT NULL,
  `submission_date` date DEFAULT NULL,
  PRIMARY KEY (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userinfo`
--

LOCK TABLES `userinfo` WRITE;
/*!40000 ALTER TABLE `userinfo` DISABLE KEYS */;
INSERT INTO `userinfo` VALUES (1,'jack','123456','1538773813@qq.com','2022-09-15'),(2,'wsuncle','Qq1538773813','wsuncle@bu.edu','2022-09-15'),(3,'Huangzhe','123456','hz1538773813@qq.com','2022-09-15'),(4,'hz','123456','22333123@mm.com','2022-09-15'),(5,'Tyron','123456','tyron@bu.edu','2022-09-16');
/*!40000 ALTER TABLE `userinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userlevel`
--

DROP TABLE IF EXISTS `userlevel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userlevel` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(100) NOT NULL,
  `status` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userlevel`
--

LOCK TABLES `userlevel` WRITE;
/*!40000 ALTER TABLE `userlevel` DISABLE KEYS */;
INSERT INTO `userlevel` VALUES (1,'jack','0'),(2,'Tyron','1');
/*!40000 ALTER TABLE `userlevel` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-09-17 18:16:25
