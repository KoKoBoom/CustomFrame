/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2017/2/15 1:06:15                            */
/*==============================================================*/


drop table if exists Role;

drop table if exists RolePurview;

drop table if exists User;

drop table if exists UserRole;

drop table if exists purview;

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table Role
(
   RoleID               varchar(50) not null comment 'RoleID',
   RoleName             varchar(255) not null comment '��ɫ����',
   RoleRemark           varchar(255) comment '��ɫ��ע
            ',
   primary key (RoleID)
);

alter table Role comment '��ɫ��';

/*==============================================================*/
/* Table: RolePurview                                           */
/*==============================================================*/
create table RolePurview
(
   RolePurviewID        varchar(50) not null comment 'RolePurviewID',
   RoleID               varchar(50) not null comment 'RoleID',
   PurviewID            varchar(50) not null comment 'PurviewID',
   primary key (RolePurviewID)
);

alter table RolePurview comment '��ɫȨ�ޱ�';

/*==============================================================*/
/* Table: User                                                  */
/*==============================================================*/
create table User
(
   UserID               varchar(50) not null comment 'UserID',
   Name                 varchar(50) not null comment '�û�����',
   Password             varchar(50) not null comment '�û�����',
   Nickname             varchar(50) comment '�ǳ�',
   Age                  integer comment '����',
   Sex                  integer unsigned not null default 0 comment '�Ա� 0 : δ֪ ��1���� ��2��Ů',
   Phone                varchar(50) comment '�绰',
   CreateDate           datetime comment '����ʱ��',
   Status               integer comment '�û�״̬ 0������ 1������',
   primary key (UserID)
);

alter table User comment '�û���';

/*==============================================================*/
/* Table: UserRole                                              */
/*==============================================================*/
create table UserRole
(
   UserRoleID           varchar(50) not null comment 'UserRoleID',
   UserID               varchar(50) not null comment 'UserID',
   RoleID               varchar(50) not null comment 'RoleID',
   primary key (UserRoleID)
);

alter table UserRole comment '�û���ɫ��ϵ��';

/*==============================================================*/
/* Table: purview                                               */
/*==============================================================*/
create table purview
(
   PurviewID            varchar(50) not null comment 'PurviewID',
   PurviewName          varchar(50) not null comment 'Ȩ������',
   PurviewUrl           varchar(255) comment 'Ȩ��Url',
   PurviewIcon          varchar(255) not null comment 'Ȩ��ͼ��',
   PPurviewID           varchar(50) comment 'Ȩ�޸���ID',
   SequenceNO           varchar(50) comment 'Ȩ��˳��',
   PurviewType          varchar(50) comment 'Ȩ�����',
   primary key (PurviewID)
);

alter table purview comment 'Ȩ��';

