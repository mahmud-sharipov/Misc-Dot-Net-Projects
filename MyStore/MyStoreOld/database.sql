/*
SQLyog Ultimate v12.14 (64 bit)
MySQL - 5.6.37 : Database - MyStore1
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`MyStore1` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `MyStore1`;

/*Table structure for table `Debtor` */

DROP TABLE IF EXISTS `Debtor`;

CREATE TABLE `Debtor` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Name` longtext,
  `Description` longtext,
  `Remains` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Id` (`Id`) USING HASH,
  CONSTRAINT `FK_Debtor_Invoices_Id` FOREIGN KEY (`Id`) REFERENCES `Invoices` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Debtor` */

/*Table structure for table `Invoices` */

DROP TABLE IF EXISTS `Invoices`;

CREATE TABLE `Invoices` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Date` datetime NOT NULL,
  `Amount` double NOT NULL,
  `Price` double NOT NULL,
  `TotalPrice` double NOT NULL,
  `FromWarehouse_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Product_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FromWarehouse_Id` (`FromWarehouse_Id`) USING HASH,
  KEY `IX_Product_Id` (`Product_Id`) USING HASH,
  CONSTRAINT `FK_Invoices_Products_Product_Id` FOREIGN KEY (`Product_Id`) REFERENCES `Products` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Invoices_Warehouses_FromWarehouse_Id` FOREIGN KEY (`FromWarehouse_Id`) REFERENCES `Warehouses` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Invoices` */

insert  into `Invoices`(`Id`,`Date`,`Amount`,`Price`,`TotalPrice`,`FromWarehouse_Id`,`Product_Id`) values 
('124bd0f9-8309-48b5-954f-9e8246ba6397','2018-05-26 10:59:30',3,77,231,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','b0f1e526-42f9-44a3-9d5b-9154af67554f');

/*Table structure for table `ProductTypes` */

DROP TABLE IF EXISTS `ProductTypes`;

CREATE TABLE `ProductTypes` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Name` longtext,
  `Description` longtext,
  `Base_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Base_Id` (`Base_Id`) USING HASH,
  CONSTRAINT `FK_ProductTypes_ProductTypes_Base_Id` FOREIGN KEY (`Base_Id`) REFERENCES `ProductTypes` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ProductTypes` */

insert  into `ProductTypes`(`Id`,`Name`,`Description`,`Base_Id`) values 
('13d9a7b2-511d-4129-ab9c-89108c6d94c3','Ð¼Ð°ÑˆÑ…Ð°Ð´','','db8d22bb-4785-4ecf-b9c5-cea5e83d5f03'),
('2bf31926-6fcc-4236-8f4a-b421855c8928','Ð¿Ð»Ð¸Ð½Ñ‚ÑƒÑ Ð¿Ð¾Ñ‚Ð¾Ð»Ð¾Ñ‡Ð½Ð¸Ð¹','',NULL),
('6964b43f-6373-4661-ab38-6adc807ca63c','Ð´ÐµÐºÐµÑ€Ñ‚','','db8d22bb-4785-4ecf-b9c5-cea5e83d5f03'),
('8d73dbc2-f68c-471e-b2ef-c75571e3cb2c','Ð³Ð»Ð¾Ð±ÑƒÑ','','db8d22bb-4785-4ecf-b9c5-cea5e83d5f03'),
('a84ed5ec-c958-4fb1-a01c-f0e86077afa7','ÐºÑ€Ð°ÑÐ°Ð²Ð°','','db8d22bb-4785-4ecf-b9c5-cea5e83d5f03'),
('d9f5b0e8-bcc2-44f7-9a59-c8f74f62def2','Ð°Ñ‚Ð»Ð°Ñ','','db8d22bb-4785-4ecf-b9c5-cea5e83d5f03'),
('db8d22bb-4785-4ecf-b9c5-cea5e83d5f03','ÐºÑ€Ð°ÑÐºÐ°','',NULL);

/*Table structure for table `Products` */

DROP TABLE IF EXISTS `Products`;

CREATE TABLE `Products` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Name` longtext,
  `Amount` double NOT NULL,
  `Price` double NOT NULL,
  `Number` int(11) NOT NULL,
  `Code` longtext,
  `Type_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `UOM_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Type_Id` (`Type_Id`) USING HASH,
  KEY `IX_UOM_Id` (`UOM_Id`) USING HASH,
  CONSTRAINT `FK_Products_ProductTypes_Type_Id` FOREIGN KEY (`Type_Id`) REFERENCES `ProductTypes` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Products_UOMS_UOM_Id` FOREIGN KEY (`UOM_Id`) REFERENCES `UOMS` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Products` */

insert  into `Products`(`Id`,`Name`,`Amount`,`Price`,`Number`,`Code`,`Type_Id`,`UOM_Id`) values 
('0810c98d-69e7-4918-9722-30e55ad082b5','',0,0,1,'','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c',NULL),
('08a760aa-55c4-4384-b25b-a1653a7e90ca','',0,0,1,'','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c',NULL),
('10c55777-8959-4282-a189-3c0ce8c971d0','ÑÐ°Ñ„ÐµÐ´ 3Ð»',7,0,2,'','d9f5b0e8-bcc2-44f7-9a59-c8f74f62def2','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('13fd8938-d5e6-4ed8-a1d4-6ad556cacebb','ÑÐ¸Ð½Ð¸Ð¹ 3Ð»',6,0,7,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('1c69e278-5c89-47de-b6d2-79063ec6c455','',0,0,1,'','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c',NULL),
('24f81292-98b5-42e8-b803-1ff53cb78c8b','I-40 K-3',0,4.5,6,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('2a99641b-e63b-43c0-94ac-5c8cc10ce099','AM-50 K-2',0,7,4,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('3e6f0696-6dff-429e-87de-5d96ddcdb431','ÑÐ°Ñ„ÐµÐ´ 3Ð»',6,0,1,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('3fc1b689-396e-43ac-b214-920660f82618','Ð³Ð»Ð¾Ð±ÑƒÑ 3Ð» Ð¿Ð¾Ð»Ð°Ð²Ð¾Ð¹',0,0,2,'0,68','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('4109bf8d-6629-4e2e-9cef-937ad1e978f0','Ð³Ð»Ð¾Ð±ÑƒÑ 3Ð» ÑÐ°Ñ„ÐµÐ´',0,0,1,'0,64','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('4addcd11-1d93-4f20-b835-85c72bef69bf','',0,0,1,'','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c',NULL),
('4c2587f4-08b5-4c80-a77b-8be4d20f75ff','',0,0,1,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3',NULL),
('50f46974-5206-4800-82c7-211072f39fa4','',0,0,1,'','6964b43f-6373-4661-ab38-6adc807ca63c',NULL),
('5514ef62-3fd2-42aa-9172-a909f5bab47f','Ð²Ð¸ÑˆÐ½ÐµÐ²Ð°Ñ 3Ð»',6,0,9,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('5acd0775-ad3b-4f71-b9f5-c2610a463c57','ÐºÑ€Ð°ÑÐ°Ð²Ð° 3Ð» ÑÐ°Ñ„ÐµÐ´',5,0,1,'','a84ed5ec-c958-4fb1-a01c-f0e86077afa7','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('5c1db7f6-00ba-47ed-981e-239de437a6b2','ÑÐ°Ð±Ð· 3Ð»',3,0,4,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('61395376-c00f-4841-9110-d62c21cd8b38','Ð¼Ð°ÑˆÑ…Ð°Ð´ 4Ð» Ð²Ð¸ÑˆÐ½Ñ',0,77,5,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('64c6ca1d-c7a5-4bf2-bdff-94357196aa83','Ð³Ð»Ð¾Ð±ÑƒÑ 4Ð» ÑÐ°Ñ„ÐµÐ´',0,0,2,'','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('657d5e08-de08-4a5b-8e87-eaa87f4f0602','ÑˆÐ¾ÐºÐ¾Ð»Ð°Ð´Ð½Ð¸',1,0,12,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('6d5f4b9b-e761-4393-b05b-c7dcbb6c68a0','Ð¶ÐµÐ»Ñ‚Ð¾-ÐºÐ¾Ñ€Ð¸Ñ‡Ð½Ðµ 3Ð» ',11,0,2,'','a84ed5ec-c958-4fb1-a01c-f0e86077afa7','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('6ea159dd-6fe8-47e0-a3cd-522538c50691','X-04 K-17',0,12,7,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('6ed01e13-c994-461c-98f6-1acb6ee25f3c','',0,0,1,'','6964b43f-6373-4661-ab38-6adc807ca63c',NULL),
('71cd0dfa-2971-4aa6-9279-1c4c2839075c','ÐºÐ¾Ñ€Ð¸Ñ‡Ð½Ð¾Ð²Ð¸',2,0,13,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('74397d2d-37bd-4433-bbc5-d0d85bb6fe01','Ð¼Ð°ÑˆÑ…Ð°Ð´ ÑÐµÑ€ÐµÐ±Ñ€Ð¾',5,0,12,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('768a908a-0966-488a-8429-cf26f58a2db9','Ð¿Ð¾Ð»Ð°Ð²Ð¾Ð¹ 3Ð» ',6,0,1,'','d9f5b0e8-bcc2-44f7-9a59-c8f74f62def2','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('79bc8dd3-8a21-4892-b0d6-fc969a42405a','ÑÐ¸Ñ‘Ò³ 3Ð»',2,0,4,'','a84ed5ec-c958-4fb1-a01c-f0e86077afa7','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('81b3080c-e7cc-4559-b376-d4888ea8a804','ÑÐ²ÐµÑ‚Ð»Ð¾ Ð³Ð¾Ð»ÑƒÐ±Ð¾Ñ 3Ð»',6,0,6,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('91e2c54a-a1a5-4c38-b7f8-5df7b77e73e2','Ð¼Ð°ÑˆÑ…Ð°Ð´ 4Ð» Ð¿Ð¾Ð»Ð°Ð²Ð¾Ð¹',5,77,1,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('98b27433-eef7-4193-af39-0120e6bafe71','Ð¼Ð°ÑˆÑ…Ð°Ð´ Ð²Ð¸ÑˆÐ½Ñ 1Ð»',2,0,11,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('9a8859ee-c403-4f03-be8e-23b6f8f9a4cf','Ð¿Ð¾Ð»Ð°Ð²Ð¾Ð¹ ÐºÑ€Ð°ÑÐ½Ð¾-ÐºÐ¾Ñ€Ð¸Ñ‡ 3Ð»',8,0,2,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('a02de8df-40af-4f5a-a8c4-45c4df3e5372','',0,0,1,'','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c',NULL),
('a14a8474-3bd9-4ef1-81e1-1ef8aad00431','ÐºÐ¾Ñ€Ð¸Ñ‡ 3Ð»',9,0,5,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('a27d62dc-d176-4238-b005-124ab9947cf1','',0,0,1,'','2bf31926-6fcc-4236-8f4a-b421855c8928',NULL),
('ac653b4d-81d0-4f08-8a74-bd300af9fde7','L-60 K-6',0,8,2,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('acf4d905-3119-45c8-9831-dcd10a1906c0','ÑÐ¸Ð½Ð¸Ð¹ 1Ð»',3,0,11,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('b0252ec7-2542-4534-be14-c2489c1835bf','G-2 K-16',0,12,9,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('b0f1e526-42f9-44a3-9d5b-9154af67554f','Ð¼Ð°ÑˆÑ…Ð°Ð´ 4Ð» ÑÐ°Ñ„ÐµÐ´',3,77,2,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('b8798ad0-45e8-4089-9243-e697ce50aea3','B-13 K-7',0,10,5,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('c2254c3d-6358-4d45-a9a2-3f98c43675b3','B-4 K-15',0,8,8,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('c22cd6ec-043b-4017-9020-25d6730b6b27','Ð¼Ð°ÑˆÑ…Ð°Ð´ 4Ð» ÑÐµÑ€ÐµÐ±Ñ€Ð¾',0,77,4,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('c9a3c3e8-9c76-4a9c-b663-a4388bba8e4b','',0,0,1,'','2bf31926-6fcc-4236-8f4a-b421855c8928',NULL),
('cb667ccb-bc78-45c8-8815-597b93972582','ÐºÑ€Ð°ÑÐ½Ð¾-ÐºÐ¾Ñ€Ð¸Ñ‡ 3Ð» ',11,0,3,'','a84ed5ec-c958-4fb1-a01c-f0e86077afa7','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('ce21c089-11c6-4e18-9831-8aa8ec12164a','Ð³Ð»Ð¾Ð±ÑƒÑ 3Ð» ÑÐ¸Ñ‘Ò³',2,0,4,'0,64','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('cf3bad77-e4a7-45de-bd88-15dd2e7956ad','',0,0,1,'','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c',NULL),
('d6d63621-7152-4b5b-aceb-7a67b5faec9d','Ð¼Ð°ÑˆÑ…Ð°Ð´ 4Ð» Ð»Ð°Ðº',2,77,3,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('da02f682-fad1-40e9-a86f-1ff91ff2bd5e','ÐºÐ°Ð±ÑƒÐ´',0,0,3,'','6964b43f-6373-4661-ab38-6adc807ca63c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('dc9f9b99-265f-4c69-8d5c-b58b34e19dbd','ÑÐ°Ð±Ð·',0,0,5,'','a84ed5ec-c958-4fb1-a01c-f0e86077afa7','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('e3426998-c838-49da-819f-63204a8f50fc','Ð³Ð»Ð¾Ð±ÑƒÑ 4Ð» Ð¿Ð¾Ð»Ð°Ð²Ð¾Ð¹',7,53,1,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('e56f7c0f-c849-4e6c-9cfe-a084d1ebaf03','',0,0,1,'','a84ed5ec-c958-4fb1-a01c-f0e86077afa7',NULL),
('f0d4479f-b0db-42d6-a920-17eaf8a2f518','Ð¼Ð°ÑˆÑ…Ð°Ð´ 3Ð» silver',0,0,7,'','13d9a7b2-511d-4129-ab9c-89108c6d94c3','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('f448e46c-4eb1-49ed-8e70-a8377134eba4','Ð³Ð»Ð¾Ð±ÑƒÑ 3Ð» ÐºÐ°Ð±ÑƒÐ´',16,0,3,'0,64','8d73dbc2-f68c-471e-b2ef-c75571e3cb2c','e9f58c58-d7cc-4b9e-ad00-e603f7df3067'),
('f475ea16-081b-43d5-a2ec-5f8231297d53','B-45 K-1',0,7,3,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('f73093f5-a1c6-410e-b96d-7c115f1839de','G-15 K-5',0,7.5,1,'','2bf31926-6fcc-4236-8f4a-b421855c8928','106148e6-9309-496e-a825-40f9b0d2aea8'),
('ff631e58-bb3d-458a-b85c-99a31c29d5ec','ÑÐµÑ€Ð¸',0,0,6,'','a84ed5ec-c958-4fb1-a01c-f0e86077afa7','e9f58c58-d7cc-4b9e-ad00-e603f7df3067');

/*Table structure for table `Suppliers` */

DROP TABLE IF EXISTS `Suppliers`;

CREATE TABLE `Suppliers` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Name` longtext,
  `Description` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Suppliers` */

/*Table structure for table `Supplies` */

DROP TABLE IF EXISTS `Supplies`;

CREATE TABLE `Supplies` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Price` double NOT NULL,
  `Amount` double NOT NULL,
  `Date` datetime NOT NULL,
  `Supplier_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `ToWarehouse_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Product_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Supplier_Id` (`Supplier_Id`) USING HASH,
  KEY `IX_ToWarehouse_Id` (`ToWarehouse_Id`) USING HASH,
  KEY `IX_Product_Id` (`Product_Id`) USING HASH,
  CONSTRAINT `FK_Supplies_Products_Product_Id` FOREIGN KEY (`Product_Id`) REFERENCES `Products` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Supplies_Suppliers_Supplier_Id` FOREIGN KEY (`Supplier_Id`) REFERENCES `Suppliers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Supplies_Warehouses_ToWarehouse_Id` FOREIGN KEY (`ToWarehouse_Id`) REFERENCES `Warehouses` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Supplies` */

insert  into `Supplies`(`Id`,`Price`,`Amount`,`Date`,`Supplier_Id`,`ToWarehouse_Id`,`Product_Id`) values 
('19e5190f-5876-421d-aa25-c81bf9cc6518',77,3,'2018-05-26 10:58:59',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','b0f1e526-42f9-44a3-9d5b-9154af67554f'),
('1e9f19e0-4dd0-4e25-8774-fa4cdab5ce55',0,5,'2018-05-26 16:39:36',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','5acd0775-ad3b-4f71-b9f5-c2610a463c57'),
('26907373-c826-40c3-8565-f02f0cb9eaca',0,5,'2018-05-26 11:15:26',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','74397d2d-37bd-4433-bbc5-d0d85bb6fe01'),
('2f502155-419f-4225-b6c2-54d38bf345bb',77,2,'2018-05-26 11:02:55',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','d6d63621-7152-4b5b-aceb-7a67b5faec9d'),
('3b4fa14f-7bc5-4e33-9f20-c6404f1fa98f',0,11,'2018-05-26 16:40:08',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','cb667ccb-bc78-45c8-8815-597b93972582'),
('3b54c6a4-9728-4261-995c-917b19218805',0,3,'2018-05-26 18:02:39',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','acf4d905-3119-45c8-9831-dcd10a1906c0'),
('3bbcb062-4f71-4206-85da-3cd36acf0ab2',0,6,'2018-05-27 12:38:22',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','768a908a-0966-488a-8429-cf26f58a2db9'),
('43291817-97b8-4841-b9d5-e2616bd36e0f',0,6,'2018-05-26 18:01:50',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','81b3080c-e7cc-4559-b376-d4888ea8a804'),
('4c90e7ba-617b-4d97-8f71-44a029d62d9b',0,8,'2018-05-26 17:51:51',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','9a8859ee-c403-4f03-be8e-23b6f8f9a4cf'),
('524022fe-30a3-47d7-9f77-5a68c696f654',0,1,'2018-05-26 18:02:47',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','657d5e08-de08-4a5b-8e87-eaa87f4f0602'),
('55293f02-b299-43b8-bba0-d269f77af46d',0,11,'2018-05-26 16:39:49',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','6d5f4b9b-e761-4393-b05b-c7dcbb6c68a0'),
('6df27df4-7a46-4285-b01d-cab2ca83b533',77,3,'2018-05-26 10:58:24',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','b0f1e526-42f9-44a3-9d5b-9154af67554f'),
('6eed162d-19cb-45a1-aa14-ee89fc4c87d5',0,2,'2018-05-26 16:32:33',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','ce21c089-11c6-4e18-9831-8aa8ec12164a'),
('84409914-46bb-4901-bc3f-73d6b2fc4f0d',77,5,'2018-05-26 10:05:12',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','91e2c54a-a1a5-4c38-b7f8-5df7b77e73e2'),
('85f630bd-f157-4574-b07e-038963306220',0,2,'2018-05-26 18:02:56',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','71cd0dfa-2971-4aa6-9279-1c4c2839075c'),
('875c0cbb-75ca-488b-a212-23b33a8226a2',0,6,'2018-05-26 17:49:07',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','3e6f0696-6dff-429e-87de-5d96ddcdb431'),
('884a34f9-3401-4edd-b88c-a72f0b5d1b00',0,0,'2018-05-26 17:33:14',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','dc9f9b99-265f-4c69-8d5c-b58b34e19dbd'),
('8f82cdcc-de54-46b8-b3a7-fbe33c87f97d',0,0,'2018-05-26 17:43:38',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','3fc1b689-396e-43ac-b214-920660f82618'),
('917400fc-1d6a-40d2-8bd7-8b91d7d703c4',77,0,'2018-05-26 10:06:06',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','91e2c54a-a1a5-4c38-b7f8-5df7b77e73e2'),
('95841d7b-27cd-41ef-bc7a-4c4340024480',0,16,'2018-05-26 16:32:14',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','f448e46c-4eb1-49ed-8e70-a8377134eba4'),
('a58bb06a-282c-4c9f-9059-7f569dd05277',0,9,'2018-05-26 18:01:32',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','a14a8474-3bd9-4ef1-81e1-1ef8aad00431'),
('aa0b8d5e-b893-46f8-a3cb-4c21ecb42ff3',0,0,'2018-05-26 17:43:25',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','3fc1b689-396e-43ac-b214-920660f82618'),
('abafee92-f6d6-4633-a5b9-aa718cbb4170',0,6,'2018-05-26 18:02:01',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','13fd8938-d5e6-4ed8-a1d4-6ad556cacebb'),
('aecf0235-7601-414a-aa6b-60cf175e9280',0,0,'2018-05-26 17:34:14',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','3fc1b689-396e-43ac-b214-920660f82618'),
('b89a320d-4e92-450a-828d-fe5e936be841',0,2,'2018-05-26 11:15:03',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','98b27433-eef7-4193-af39-0120e6bafe71'),
('ba1da8a2-bbed-4275-9c4f-23a0ff852317',0,6,'2018-05-26 18:02:21',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','5514ef62-3fd2-42aa-9172-a909f5bab47f'),
('d161e2a5-5b82-437c-b553-0fd9ca096414',0,3,'2018-05-26 18:01:19',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','5c1db7f6-00ba-47ed-981e-239de437a6b2'),
('f16da889-c677-4c98-83a2-89bac5fb3be3',0,7,'2018-05-27 12:38:34',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','10c55777-8959-4282-a189-3c0ce8c971d0'),
('f685931a-c027-496e-9fce-3b1672e80281',0,7,'2018-05-26 15:03:03',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','e3426998-c838-49da-819f-63204a8f50fc'),
('fdf48c80-824a-498e-98f1-2248732a323b',0,2,'2018-05-26 16:40:39',NULL,'7953f1cd-2b87-48b7-a04c-585bc258f0bd','79bc8dd3-8a21-4892-b0d6-fc969a42405a');

/*Table structure for table `UOMS` */

DROP TABLE IF EXISTS `UOMS`;

CREATE TABLE `UOMS` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Name` longtext,
  `Description` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `UOMS` */

insert  into `UOMS`(`Id`,`Name`,`Description`) values 
('106148e6-9309-496e-a825-40f9b0d2aea8','Ð¼','ÐœÐµÑ‚Ñ€'),
('1545a906-1012-4139-853d-eaae5f3dd105','ÐºÐ³','ÐšÐ»Ð¾Ð³Ñ€Ð°Ð¼Ð¼'),
('286febd7-938a-4f0c-b837-a9727f4f5941','Ð¼3','ÐšÑƒÐ±Ð¸Ñ‡ÐµÑÐºÑ‹Ð¹ Ð¼ÐµÑ‚Ñ€'),
('4280aedc-0662-4f7f-8510-4e7b78a3e5d4','ÑÐ¼2','ÐšÐ²Ð°Ð´Ñ€Ð°Ñ‚Ð½Ñ‹Ð¹ ÑÐ°Ð½Ñ‚Ð¸Ð¼ÐµÑ‚Ñ€'),
('7638740d-fae5-4350-a6e6-c6e6d9e7e28a','Ñ‚','Ð¢Ð¾Ð½Ð½Ð°'),
('79bb0d93-15ec-41ba-bff2-b4b176a87f19','Ð¼2','ÐšÐ²Ð°Ð´Ñ€Ð°Ñ‚Ð½Ñ‹Ð¹ Ð¼ÐµÑ‚Ñ€'),
('e9f58c58-d7cc-4b9e-ad00-e603f7df3067','Ð»','Ð›Ð¸Ñ‚Ñ€');

/*Table structure for table `Users` */

DROP TABLE IF EXISTS `Users`;

CREATE TABLE `Users` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Name` longtext,
  `FirstName` longtext,
  `MiddleName` longtext,
  `Username` longtext,
  `Password` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Users` */

insert  into `Users`(`Id`,`Name`,`FirstName`,`MiddleName`,`Username`,`Password`) values 
('7388ec97-93cc-4487-a07b-0826e47d260a','','','','admin','1156371652');

/*Table structure for table `WarehouseProducts` */

DROP TABLE IF EXISTS `WarehouseProducts`;

CREATE TABLE `WarehouseProducts` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Amount` double NOT NULL,
  `Product_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Warehouse_Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Product_Id` (`Product_Id`) USING HASH,
  KEY `IX_Warehouse_Id` (`Warehouse_Id`) USING HASH,
  CONSTRAINT `FK_WarehouseProducts_Products_Product_Id` FOREIGN KEY (`Product_Id`) REFERENCES `Products` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_WarehouseProducts_Warehouses_Warehouse_Id` FOREIGN KEY (`Warehouse_Id`) REFERENCES `Warehouses` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `WarehouseProducts` */

insert  into `WarehouseProducts`(`Id`,`Amount`,`Product_Id`,`Warehouse_Id`) values 
('035794eb-6d3a-4fcd-81a9-6d9d5bc0ba45',0,'b0252ec7-2542-4534-be14-c2489c1835bf','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('0613df4f-0522-45b4-b16c-65aefc4df1fb',0,'4109bf8d-6629-4e2e-9cef-937ad1e978f0','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('0cb3540e-2c1c-4b66-9e26-63e90d51faff',2,'d6d63621-7152-4b5b-aceb-7a67b5faec9d','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('0e0a8462-a109-41dd-871c-5753df615ab0',0,'f0d4479f-b0db-42d6-a920-17eaf8a2f518','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('1274c27f-5ce5-44d5-bbba-d0ce8fe43723',2,'71cd0dfa-2971-4aa6-9279-1c4c2839075c','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('127e8e9b-c813-4713-9329-893230ec7c63',0,'1c69e278-5c89-47de-b6d2-79063ec6c455','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('12a5c92b-ad62-49fa-b6be-3a5b0fcbe936',11,'cb667ccb-bc78-45c8-8815-597b93972582','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('1702b316-43fa-43e7-b9b2-71596cd4c0aa',0,'c2254c3d-6358-4d45-a9a2-3f98c43675b3','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('19969fb9-ad8c-4f62-85d5-1599e056bee7',0,'4addcd11-1d93-4f20-b835-85c72bef69bf','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('21eea9b0-16cb-4714-8de3-0ca31ee56705',0,'c22cd6ec-043b-4017-9020-25d6730b6b27','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('224ba860-0382-4b7f-907e-06913c7f61a3',6,'768a908a-0966-488a-8429-cf26f58a2db9','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('239d0ac1-f637-43d7-8465-f013a31d4d5a',0,'0810c98d-69e7-4918-9722-30e55ad082b5','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('295f7b38-2762-48f7-a7e5-58afc8b9068d',0,'da02f682-fad1-40e9-a86f-1ff91ff2bd5e','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('35d89ba7-d4f8-40be-85e2-8901d0d5491d',2,'98b27433-eef7-4193-af39-0120e6bafe71','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('398fe000-8205-4a3d-8728-f555d6d9310a',0,'f73093f5-a1c6-410e-b96d-7c115f1839de','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('3fb67b84-ca25-49c3-803e-94ab6a39cd8c',5,'91e2c54a-a1a5-4c38-b7f8-5df7b77e73e2','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('4ecccf78-bb9b-4386-9cec-35d41b134417',0,'dc9f9b99-265f-4c69-8d5c-b58b34e19dbd','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('53c5cd8f-f7bf-4b71-9c43-8536a6fc4734',7,'e3426998-c838-49da-819f-63204a8f50fc','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('57522d01-0a21-440c-9788-f888e91c62b9',3,'acf4d905-3119-45c8-9831-dcd10a1906c0','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('67611b87-d903-40f0-8e59-2dad01e1e016',0,'2a99641b-e63b-43c0-94ac-5c8cc10ce099','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('784c2fa1-e043-4ad1-b2b4-edb0c53a9de7',0,'61395376-c00f-4841-9110-d62c21cd8b38','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('79f5597c-83ca-4ecd-b0fd-2a5f85ce8d76',9,'a14a8474-3bd9-4ef1-81e1-1ef8aad00431','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('84eafacc-64ac-4517-ae16-e3d00138d9be',0,'a02de8df-40af-4f5a-a8c4-45c4df3e5372','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('871a05fd-70ef-433c-8988-66d2ee1289db',6,'3e6f0696-6dff-429e-87de-5d96ddcdb431','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('88a3b45c-35ec-4856-b5f5-5b66b06138b2',7,'10c55777-8959-4282-a189-3c0ce8c971d0','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('8af49758-0267-4ee7-aa21-0b8fe761de23',0,'ac653b4d-81d0-4f08-8a74-bd300af9fde7','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('8f02bcc9-1187-4c42-80de-ea8e68a16e00',0,'4c2587f4-08b5-4c80-a77b-8be4d20f75ff','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('909b8569-ded3-4d8f-bbb7-5fb266463c3d',0,'b8798ad0-45e8-4089-9243-e697ce50aea3','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('91e89310-bf0b-4a39-9bfe-5b0f6232c0c2',6,'5514ef62-3fd2-42aa-9172-a909f5bab47f','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('9bf28746-f569-4216-a63f-264444f3dc19',0,'f475ea16-081b-43d5-a2ec-5f8231297d53','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('9c4462ce-edc1-4828-a423-1ad014ad45f6',3,'5c1db7f6-00ba-47ed-981e-239de437a6b2','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('a5e777c6-79b4-4aed-8050-49e617c8451c',2,'ce21c089-11c6-4e18-9831-8aa8ec12164a','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('ac6b797f-199f-4cd9-976b-f7c87e688c7b',0,'64c6ca1d-c7a5-4bf2-bdff-94357196aa83','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('b356d57a-900c-43e1-8071-9fc0e02eab9f',0,'08a760aa-55c4-4384-b25b-a1653a7e90ca','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('b4f6ed60-5e53-440b-8741-1b681bf8d63c',11,'6d5f4b9b-e761-4393-b05b-c7dcbb6c68a0','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('b59dcf11-8fd7-4350-ab85-294d212a0267',0,'a27d62dc-d176-4238-b005-124ab9947cf1','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('b668521f-105c-4a2c-8153-ec050d348699',0,'3fc1b689-396e-43ac-b214-920660f82618','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('c87ace15-e653-4547-8ca9-58b51139b262',3,'b0f1e526-42f9-44a3-9d5b-9154af67554f','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('cf46ca4b-cd52-4cd0-918d-76c4f96a73b7',8,'9a8859ee-c403-4f03-be8e-23b6f8f9a4cf','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('cfbbed10-f153-47ef-9598-94ddb2361255',0,'cf3bad77-e4a7-45de-bd88-15dd2e7956ad','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('d0828f19-c2c3-497b-bc1c-45f28727f169',16,'f448e46c-4eb1-49ed-8e70-a8377134eba4','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('d95078ee-1e0a-4502-bfb4-303553955794',0,'50f46974-5206-4800-82c7-211072f39fa4','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('dfd8c0da-e5be-4930-ab3c-8ae226b7d4a2',1,'657d5e08-de08-4a5b-8e87-eaa87f4f0602','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('e234219f-d9fa-471e-9db7-882c53134a98',6,'81b3080c-e7cc-4559-b376-d4888ea8a804','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('e91970cf-f95a-4c9d-9bae-cf7c4691070d',0,'24f81292-98b5-42e8-b803-1ff53cb78c8b','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('edabb6bd-e1ba-48b4-8904-55eb86a0bc19',5,'74397d2d-37bd-4433-bbc5-d0d85bb6fe01','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('efa5b586-f7b6-4c55-896c-1f38fe2b7a5d',0,'6ea159dd-6fe8-47e0-a3cd-522538c50691','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('f107fd43-1f11-488d-ba6a-deb5b6ecc6dc',0,'e56f7c0f-c849-4e6c-9cfe-a084d1ebaf03','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('f156a808-b4e4-487d-92f4-8e45d4548800',5,'5acd0775-ad3b-4f71-b9f5-c2610a463c57','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('f3b97c94-4a81-465e-b957-be1634e8751e',0,'c9a3c3e8-9c76-4a9c-b663-a4388bba8e4b','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('f91c8b8a-0df2-49d5-8650-4b49de842c63',2,'79bc8dd3-8a21-4892-b0d6-fc969a42405a','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('fa6c76c7-9d38-40e5-ad9c-cf96f4bc90d0',0,'ff631e58-bb3d-458a-b85c-99a31c29d5ec','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('fdefc99b-278e-46d5-92a5-8ae3661ff79b',6,'13fd8938-d5e6-4ed8-a1d4-6ad556cacebb','7953f1cd-2b87-48b7-a04c-585bc258f0bd'),
('ffd19792-c16a-4d06-be77-d907ba0dc09a',0,'6ed01e13-c994-461c-98f6-1acb6ee25f3c','7953f1cd-2b87-48b7-a04c-585bc258f0bd');

/*Table structure for table `Warehouses` */

DROP TABLE IF EXISTS `Warehouses`;

CREATE TABLE `Warehouses` (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Name` longtext,
  `Description` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Warehouses` */

insert  into `Warehouses`(`Id`,`Name`,`Description`) values 
('7953f1cd-2b87-48b7-a04c-585bc258f0bd','Ð¼Ð°Ð³Ð°Ð·Ð¸Ð½','');

/*Table structure for table `__MigrationHistory` */

DROP TABLE IF EXISTS `__MigrationHistory`;

CREATE TABLE `__MigrationHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ContextKey` varchar(300) NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `__MigrationHistory` */

insert  into `__MigrationHistory`(`MigrationId`,`ContextKey`,`Model`,`ProductVersion`) values 
('201805260444299_InitialCreate','Server.EntityContext','‹\0\0\0\0\0\0í]_oã¸/Ðï è±Ø‹“]´èööœÝCÐË&Xg¯}‹q„Ê’«?{	Š~²>ÜGêW(%J%r(’¢ey°çˆäp8óãŸÉÿý÷÷ùOÛÐùŠ’4ˆ£…{vrê:(ZÇ~mnž=|÷W÷Çþø‡ùûäüZç{WäÃ%£tá>fÙî|6K×hë¥\'Û`Äiü¬ãíÌóãÙÛÓÓïggg3„I¸˜–ãÌ?çQlQùþsGk´Ër/¼Š}¦Õwœ²*©:Ÿ¼-JwÞ\Z-ÜJ0»®ó>¼´ø3|p/ŠâÌË0oç_R´Ê’8Ú¬vøƒÞ>ïÎ÷à…)ªx>o²«²ú¶`Ö¬I­ó4‹·šÏÞUò˜u‹IÕ¥òÂû€%›=­.¥¶p±@°¸º/Ã¤ÈTô„Hþ¤ÈýÆ!ßÞP…c\\ÿ½q–y˜å	ZD(Ï/|ãÜä÷a°þz¾ÿ‰¢E”‡!Ëf§µ>àO7I¼CIöü=TL^ú®3k—›uÒbLÒ„Ÿó\0ÿþ„ëöîCD•=“/þ­	`Ä`À»Î•÷ôŠ6ÙãÂÅ?]çcð„üúKEõKàþeIÞ[ÉÇ I³Qjº\n|?D£TU`$\Z£¢/M‹ßrEóYÓI¤]³ãçëL½÷T^;5¼ßÆx®¨«¹ˆ±´6§7I°Fi|Ê·÷Å@Jˆ\\FÙ»·Ú4–¸Ùö%öÉû\ZlJXvõ}qÃS×ùŒÂ2CúìÈœYCõ®Éô1‰·Ÿã°Á=M»[ÅyRJ02ÜzÉeê¬­òÝ.zXk2ñ¬Õi k4ƒ.k…z¤l‘<KÅw2Q—•/×WRNÊtžüä£HÓeãï^‚ã<‹…¦ÖUÐQ³áÊCY©Ù3Öí1\ZÇ+˜ªãU—1Ž_xY38âß·ÁVxœÌ8}‹U\Zñî³8‚¿Ê]¥|;…ëdÝ.YÔ#ï–uœ<ƒ­ Ÿí\\ºìÒ!aèDÔ/À™Êhœ¨µ¨:Lü/c”eµwÒuìˆ%¼çº>cÃ:ˆRk£d†]|£ˆº™Q@à´È+Æãf¶€á\\U<£hÎbN²M6Á2“¦ró™ ‹UÛ¥L|¾»…räS9!\n²ØéõÚÎŒnÉ—1˜-dV>ƒ,\'Ø‰eÙÚSí€ …Ç÷Q#|“þ¢Žj’ÿe`Ù†=eÅ°331™JÎ#Èœà¼KšsƒØX¬…¼q‰â™¡•C—»Ö¬dcòêB’ùÍ¼s:ûWu‰—ÑÁ¿ákÿ*K½\'	a*ìkC¶‰ˆ_Xs«¨HzÅê‘cõ\'P%ß‘LÜdDÓ —}“Aw%·|B_ºÅ¢Æ0Kòœ[1ñ”¶Y ‰»µcÔ™Ë=åh‰ë«×Î{äW	Œ’6Šì.œ\nß§i¼JÚ>›ÚkØnÏ‡Èwä;\r^ÿ#Fd€g¼5®áž¹]¬]G(DrÞ¯I0ÓÒK×žÏK·ÁWä‡Ê§á§vƒ¶Ù9=99ã*Â¨G	ŠŠH®%ÖîGA”ñ]$ˆÖÁÎ¥é”RìZEc)ýnÊÚ¡ÈÇìI›n^1¥ß‘~ŸTæ3MrNH½ý‰FÑ¼ó©­ò?qúV¨G\0(€¼=€›´ñô6Qà¯q¤þ+sâ«VC€üââT²ÒomlHÁp³JLù[Ï–¥Ö¶Ý¥ü†¼&ƒ}teÀ™Doo}ôMmÀžyïŒ+“^ÏËTÈmßCíƒƒÊFÇå	„­¦Hyõnp¤Û¡å5æ°Æª=Ì	ÌiN\\Ä²Tï™°A¬DÚêDc€´’§>¶ƒ{¦ ®Ž¡®$ªƒÊ©°ñúÚÔ\n­´\"¥–	ÙP,\rÎCàˆ§ØªÈ°M’JFÍWô$ÚBþ’¢ÊE’Vöw·ÝEùÊ˜#Øân,áºÙåaNhíÂÅÎ•§Šè!Ñ„Cp$èˆßC‚Øx\"\nµyÛC€Îã\"\ZÌ$¯JF\"~±ÞC´ñ«s´ê^‚P@Í’\\MÓ%®`m“‘¤‡îPBÀý¬S”ébh“IäÕí©½Ê1OÎ\'ÐëgaÈPˆvÇ‚vZ‡*ðrPó(èù˜F	ð.R¯¡\"/&LB\")ÀU ê,IGQ,¼¯#jÉ¶îxÁô¸\0\0LK˜ÑH\"ØpçIY“Eg¿†Ä²WµíÚ jÎ+Î@ @ \"<ÖJÌv\rÃÝtä›êûgiä\"·ÆÕìq…QÍŒVë\01ÈÆ¹‘­ff›Ša¤±ƒßÁÅ\0Ïjæ3/†j5Ö/\nÞ`V$6\0d-#‚7†ûÍaS$´àý7½\\ÊÂ-çÌÛ^×´Ý¬IËÐ +påæÖû¤Ô¥ió¹¢ú0Ÿ÷GÌ¯¼Ý.ˆ6Ì}ÕgE.“X~·Ò¿qaKhÌÖ­a¥k8Óšð\nÜÛ N*9\\ÞpáeÞ}Ù—þ–ËÖ6¼§®‹µ­y5Õ–O»ø]­CHPCi|ó›•Uþ¸Ûb«³hbõ*,æ—wx¡—–q˜o#xã.MâØòä‹:æF–óY{çKŒý®N­¹V¥Õ|U§ÔÜ›ÀRj¾ò”æ³Ž–»ð™qøáÜKm0*AUböô¢°*\0–œ*fël–FýM$\Z¼‰Dàé’¶¥º¡Õšê›:rKƒ|™.%Ëç^\\ÖþC}\\‚%÷ƒK”Ï–\'_Ž—ì©o–û}2èªÝràû°œê×Šla”MP§GOû²´èÇÉ@ƒq ô„(\0DRö[ÃÈ¡u;dÕÃíäÐôÈë ½Ùâ@J’xŒzuSíŒék*¸=Ø˜oíÌüê+Ã¢Ál®¦»œ†€}‹m\ZžÄgn|Š¼‰ê¨¸ô«†­h¸ô6š8Áx7©ŠLTêU“zšl»‚å‹§&\0¡w‰ÔdÕ]	¾íŽìúâ	x¡)A€î¡	0là<cR˜³®_ß\\ßL\0¬i&“Ms‘†²kÚÔ/ºqš€€K\'&ÚšY /0>ÄP\\”ž]‚\'£¨Ò)o^÷ƒ2ö2-Ž„ÒÃ”Š-¶ƒ†N`Xµ3\rÃƒ0¬ÅPè,%[˜FðGÿbx ?y7Ÿ¢S\\ 8iTŽ¡ìÚ´,hW\Zˆt\næã óˆÉb®V(Néð+-(ðé¸Ô(™†ù,Æ£.f5-d\n¶¥E&Læ«¨²hø#`qÑ]†+IØÓ#Á6©¥ÐXk™„¿™$+ÆCHú\nÙfºj*HXì¾lÄÞQu]·ª¬L®1&&ÏP:‹úb\r\'ÕCÅÍìW6®ØÍB½gÕú7\rW¬BûßÀâbI×Á-ÿ\ZøEÜàÕóê_áI‘~Rþ\\â©­Øœ©s\\yQð€ÒŒÜDäþùä/\'µ¦ó¼Õ,MýÖ=IÀWm…p“ÒfèMJamÈÊ†ˆÑ«R¦„øG£L)uß„2¥Ó}òIŽö‹N/+íû[}sÒh? DÙ ç“LÅA—¬Z¸ãÌ—‘žî¿ËBçÎå?îªroœëžçÎ©ómþë©E³jRªy@gÚà“íìÂ>þ•wß$hG)OÞm|À¾Á?ZcHˆ÷Wi‚­K`àY+]“¦¨º½ø\nÍ(x‡à6FSR§V”°mð¬ÉqŒcÕù³Ç!\\K#ú¸ÃÏÁ !ÜpøÖ}qá8\0acRµª=,_Zû²š aÊª¯,E—APmÿ6×\ZâXÎÉv˜ã˜žÀ‹W¹ª‘¢š}¥*·k”¿~íU•\Zòz}ø/¯¯Ôbé†ð1îì©Ö4ÅKÀëôÃ]FÊ_iÇ°¢ï¸Š€m¸} HáÜ_§<JbL\\iÜ…>»Ïˆ*Ù)Îiàª/\"ndé_?…KáŸnX7B‘äô™uùšz¢§ÇBŽÚEùÓ“ÆFÎ¨ãtúã¬G@Þ{Ó\0Æ\nÿ!¤³–W‰çEta¦øhÂ×ÙãƒgÄUµt¾˜®P}9â€ {Ê\Z2\ZÖTÓD~÷ ¦‰ùUˆŠ¨Æ](\'û­Ã©ÛX•*%ó#Cßû @íMfÏu‚Ô–ÐpÐýˆÈ‘=´BƒáGzåÀXß6qPŒÀqþÖ!Ò‰˜gÂ<Û7ãvõÕyÙ£|3¦Ùi=%C\"ã®cµ’M•êñ™™œ,U<G™¦ˆˆSGg}ºˆæèÓ}ð&õ.ýz…#_\'ˆ¨ë>]#¹FTƒäJ|°X|iÊÊ©W«\\…u‚¨\Zðvw!õ@ˆÚ&IRƒ:zÉD!¸:£¸ÈÐ_W9Fóý¯<¸$ê~×W+ŽæHoîˆ%uüÞ$Sè#“{T§ÓkZÇdM†ºø:‹bØÃ[9bÞ\'Õü½=ˆ#|\0GÚôö¨Çð¶ÔL»oÝÑò>›º·Wl†4Xc\0œÌÛ4ÚC×8´ûòŒv#÷	]ÎcdíUèiS9W\Zƒ¿3´Á*˜8ÔK1Ìj^}¸ÿ&i¼Ã£Æ&j×ä¯”›†Dq.<Bë–qJó\\Fqm#w8ª³t\"û®PæùØr}ŸdÁƒ·Îp2ìÒ Ú¸Î¯^˜ã,¶÷È¿Œ®ól—g¸Éh{ßî½…­-«¿|ò¦Íóüº&Lm4³1ê×ÑOyú”ï‚0h€DaÄÿŒðw¢Ëÿmž)¥Oq¤H¨õ=Ü¢í.ÄÄÒëhå}E&¼aûÿ´ñÖÏ7Õax˜H¿\"ÚbŸ_Þ&ñ¶iE£)ÿÄö·O?ü¸AÉÄw«\0\0','6.1.3-40302');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
