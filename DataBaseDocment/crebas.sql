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
   RoleName             varchar(255) not null comment '角色名称',
   RoleRemark           varchar(255) comment '角色备注
            ',
   primary key (RoleID)
);

alter table Role comment '角色表';

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

alter table RolePurview comment '角色权限表';

/*==============================================================*/
/* Table: User                                                  */
/*==============================================================*/
create table User
(
   UserID               varchar(50) not null comment 'UserID',
   Name                 varchar(50) not null comment '用户名称',
   Password             varchar(50) not null comment '用户密码',
   Nickname             varchar(50) comment '昵称',
   Age                  integer comment '年龄',
   Sex                  integer unsigned not null default 0 comment '性别 0 : 未知 ；1：男 ；2：女',
   Phone                varchar(50) comment '电话',
   CreateDate           datetime comment '创建时间',
   Status               integer comment '用户状态 0：冻结 1：正常',
   primary key (UserID)
);

alter table User comment '用户表';

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

alter table UserRole comment '用户角色关系表';

/*==============================================================*/
/* Table: purview                                               */
/*==============================================================*/
create table purview
(
   PurviewID            varchar(50) not null comment 'PurviewID',
   PurviewName          varchar(50) not null comment '权限名称',
   PurviewUrl           varchar(255) comment '权限Url',
   PurviewIcon          varchar(255) not null comment '权限图标',
   PPurviewID           varchar(50) comment '权限父级ID',
   SequenceNO           varchar(50) comment '权限顺序',
   PurviewType          varchar(50) comment '权限类别',
   primary key (PurviewID)
);

alter table purview comment '权限';

