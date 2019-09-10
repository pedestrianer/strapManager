/*
Navicat MySQL Data Transfer

Source Server         : test
Source Server Version : 80016
Source Host           : localhost:3306
Source Database       : concentrator

Target Server Type    : MYSQL
Target Server Version : 80016
File Encoding         : 65001

Date: 2019-09-10 23:44:50
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for concentratorfiredata
-- ----------------------------
DROP TABLE IF EXISTS `concentratorfiredata`;
CREATE TABLE `concentratorfiredata` (
  `concentratoraddress` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `registeraddreess` varchar(4) NOT NULL,
  `strapaddress` varchar(10) NOT NULL,
  `straptype` varchar(1) NOT NULL,
  `isfire` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `voltage` varchar(12) NOT NULL,
  `upnumber` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of concentratorfiredata
-- ----------------------------
INSERT INTO `concentratorfiredata` VALUES ('01', '0006', '0000000390', '2', '0', '0.0', '0');
INSERT INTO `concentratorfiredata` VALUES ('01', '0006', '000000016C', '2', '0', '0.0', '0');
INSERT INTO `concentratorfiredata` VALUES ('01', '0006', '0000000267', '2', '0', '0.0', '0');

-- ----------------------------
-- Table structure for concentratortempdata
-- ----------------------------
DROP TABLE IF EXISTS `concentratortempdata`;
CREATE TABLE `concentratortempdata` (
  `concentratoraddress` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `registeraddreess` varchar(4) NOT NULL,
  `strapaddress` varchar(10) NOT NULL,
  `straptype` varchar(1) NOT NULL,
  `temperture` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `voltage` varchar(12) NOT NULL,
  `upnumber` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of concentratortempdata
-- ----------------------------
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000290', '1', '26.25', '3.3', '22');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '000000026C', '1', '24.86', '3.2', '23');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000266', '1', '27.62', '3.1', '20');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000281', '1', '27.6', '3.2', '24');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000290', '1', '26.25', '3.3', '0');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '000000026C', '1', '24.86', '3.2', '0');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000266', '1', '27.62', '3.1', '0');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000281', '1', '27.6', '3.2', '0');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000290', '1', '25.79', '3.3', '2');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '000000026C', '1', '24.86', '3.2', '2');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000266', '1', '28.27', '3.1', '2');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000281', '1', '27.6', '3.2', '2');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000290', '1', '25.32', '3.3', '1');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '000000026C', '1', '24.86', '3.2', '0');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000266', '1', '28.27', '3.1', '0');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000281', '1', '27.6', '3.2', '1');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000290', '1', '26.44', '3.3', '5');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '000000026C', '1', '24.86', '3.2', '5');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000266', '1', '27.34', '3.1', '5');
INSERT INTO `concentratortempdata` VALUES ('01', '0005', '0000000281', '1', '27.26', '3.1', '5');

-- ----------------------------
-- Table structure for data
-- ----------------------------
DROP TABLE IF EXISTS `data`;
CREATE TABLE `data` (
  `LogicAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `status` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `dianya` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `stamp` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of data
-- ----------------------------

-- ----------------------------
-- Table structure for strapmap
-- ----------------------------
DROP TABLE IF EXISTS `strapmap`;
CREATE TABLE `strapmap` (
  `devName` varchar(255) DEFAULT NULL,
  `strapaddress` varchar(10) NOT NULL,
  `type` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of strapmap
-- ----------------------------
INSERT INTO `strapmap` VALUES ('阿德', '1122334455', ' 测温 ');
INSERT INTO `strapmap` VALUES ('艾尔', '6677889900', ' 测温 ');
INSERT INTO `strapmap` VALUES ('阿里', '1323344556', ' 火灾');
INSERT INTO `strapmap` VALUES ('阿卡', '0000000266', ' 测温');
INSERT INTO `strapmap` VALUES ('艾克', '0000000267', ' 火灾');
