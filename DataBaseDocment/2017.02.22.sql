/*
SQLyog Ultimate v11.11 (64 bit)
MySQL - 5.6.24-log : Database - taki
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`taki` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;

USE `taki`;

/*Table structure for table `purview` */

DROP TABLE IF EXISTS `purview`;

CREATE TABLE `purview` (
  `PurviewID` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT 'PurviewID',
  `PurviewName` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '权限名称',
  `PurviewUrl` varchar(255) CHARACTER SET utf8 DEFAULT NULL COMMENT '权限Url',
  `PurviewIcon` varchar(255) CHARACTER SET utf8 DEFAULT NULL COMMENT '权限图标',
  `PPurviewID` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '权限父级ID',
  `SequenceNO` int(11) unsigned NOT NULL AUTO_INCREMENT COMMENT '权限顺序',
  `PurviewType` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '权限类别',
  `Status` int(1) NOT NULL DEFAULT '1' COMMENT '1.可用2.禁用3.删除',
  PRIMARY KEY (`PurviewID`),
  KEY `SequenceNO` (`SequenceNO`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COMMENT='权限';

/*Data for the table `purview` */

insert  into `purview`(`PurviewID`,`PurviewName`,`PurviewUrl`,`PurviewIcon`,`PPurviewID`,`SequenceNO`,`PurviewType`,`Status`) values ('04ef57eb-f2cb-11e6-8efb-ac220b0c81fa','角色管理','/Manage/RoleManage/',NULL,'579504d1-f2ca-11e6-8efb-ac220b0c81fa',5,'menu',1),('107ec0e1-6eb6-4ec5-8606-8cbe2cc2d04a','菜单修改','/Manage/UpdateMenu/',NULL,'82cfed81-f2ca-11e6-8efb-ac220b0c81fa',11,NULL,1),('41ffb32d-f2cc-11e6-8efb-ac220b0c81fa','权限管理',NULL,NULL,'579504d1-f2ca-11e6-8efb-ac220b0c81fa',6,'menu',1),('579504d1-f2ca-11e6-8efb-ac220b0c81fa','系统管理',NULL,NULL,NULL,2,'menu',1),('736f8fb2-83a1-4370-bb50-1e9053c93268','显示菜单主页','/Manage/Index/',NULL,NULL,1,NULL,1),('82cfed81-f2ca-11e6-8efb-ac220b0c81fa','菜单管理','/Manage/MenuManage/',NULL,'579504d1-f2ca-11e6-8efb-ac220b0c81fa',4,'menu',1),('cca901dd-bdb4-405e-bcc8-f29b2e3b5981','菜单启用/禁用','/Manage/UpdatePurviewStatus',NULL,'82cfed81-f2ca-11e6-8efb-ac220b0c81fa',10,NULL,1),('def3538d-f2ca-11e6-8efb-ac220b0c81fa','用户管理','/Manage/UserManage/',NULL,'579504d1-f2ca-11e6-8efb-ac220b0c81fa',3,'menu',1),('e766bd6e-f2cb-11e6-8efb-ac220b0c81fa','菜单三',NULL,NULL,NULL,8,'menu',1),('ed45603c-cf1d-4c43-a467-b3be72afa190','菜单删除','/Manage/DeleteMenu/',NULL,'82cfed81-f2ca-11e6-8efb-ac220b0c81fa',9,NULL,1);

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `RoleID` varchar(50) NOT NULL COMMENT 'RoleID',
  `RoleName` varchar(255) NOT NULL COMMENT '角色名称',
  `SeqNO` varchar(20) DEFAULT NULL COMMENT '排序',
  `RoleRemark` varchar(255) DEFAULT NULL COMMENT '角色备注\n            ',
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户角色表';

/*Data for the table `role` */

insert  into `role`(`RoleID`,`RoleName`,`SeqNO`,`RoleRemark`) values ('0594aa04-0df0-41a4-98be-6d528a9bd37d','超级管理员','00','超级管理员');

/*Table structure for table `rolepurview` */

DROP TABLE IF EXISTS `rolepurview`;

CREATE TABLE `rolepurview` (
  `RolePurviewID` varchar(50) NOT NULL COMMENT 'RolePurviewID',
  `RoleID` varchar(50) NOT NULL COMMENT 'RoleID',
  `PurviewID` varchar(50) NOT NULL COMMENT 'PurviewID',
  PRIMARY KEY (`RolePurviewID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='角色权限表';

/*Data for the table `rolepurview` */

insert  into `rolepurview`(`RolePurviewID`,`RoleID`,`PurviewID`) values ('27247207-c67e-4899-a2c5-5b787176c8c7','0594aa04-0df0-41a4-98be-6d528a9bd37d','579504d1-f2ca-11e6-8efb-ac220b0c81fa'),('55af2c6a-0bd2-4d85-96e8-99a96fab91e6','0594aa04-0df0-41a4-98be-6d528a9bd37d','736f8fb2-83a1-4370-bb50-1e9053c93268'),('86253ce7-c53e-4161-858d-25c5b94c9ee7','0594aa04-0df0-41a4-98be-6d528a9bd37d','82cfed81-f2ca-11e6-8efb-ac220b0c81fa'),('97658ef5-8ba0-47b5-a5e0-89cfee4e5d75','0594aa04-0df0-41a4-98be-6d528a9bd37d','def3538d-f2ca-11e6-8efb-ac220b0c81fa');

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `UserID` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT 'UserID',
  `Name` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '用户名称',
  `Password` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '用户密码',
  `Nickname` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '昵称',
  `Phone` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '电话',
  `Email` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮箱',
  `Sex` int(10) unsigned NOT NULL DEFAULT '0' COMMENT '性别 0 : 未知 ；1：男 ；2：女',
  `Birthday` date DEFAULT '1970-01-01' COMMENT '生日',
  `CreateDate` datetime NOT NULL DEFAULT '1970-01-01 00:00:00' COMMENT '创建时间',
  `Status` int(11) NOT NULL DEFAULT '1' COMMENT '用户状态 0：冻结 1：正常',
  `IsAdministrator` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='用户表';

/*Data for the table `user` */

insert  into `user`(`UserID`,`Name`,`Password`,`Nickname`,`Phone`,`Email`,`Sex`,`Birthday`,`CreateDate`,`Status`,`IsAdministrator`) values ('2a6b93cc-2fd7-4be9-9e3d-7b8b8b346452','test','test','A','123122525','1@52.com',1,NULL,'2017-02-17 23:29:50',1,0),('594c21cf-5f69-477d-bdb3-16c320e334fd','BBBB','BBBB','BB','124563555','i@i.co',1,NULL,'2017-02-18 14:53:16',1,0),('a54774a9-4049-4f04-9192-01d28bb0e2db','admin','admin','管理员','18616941345',NULL,1,NULL,'2017-02-13 01:47:37',1,1);

/*Table structure for table `userrole` */

DROP TABLE IF EXISTS `userrole`;

CREATE TABLE `userrole` (
  `UserRoleID` varchar(50) NOT NULL COMMENT 'UserRoleID',
  `UserID` varchar(50) NOT NULL COMMENT 'UserID',
  `RoleID` varchar(50) NOT NULL COMMENT 'RoleID',
  PRIMARY KEY (`UserRoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户角色关系表';

/*Data for the table `userrole` */

insert  into `userrole`(`UserRoleID`,`UserID`,`RoleID`) values ('5b542dab-299a-48c9-bd00-aae6bf0801b7','2a6b93cc-2fd7-4be9-9e3d-7b8b8b346452','0594aa04-0df0-41a4-98be-6d528a9bd37d');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
