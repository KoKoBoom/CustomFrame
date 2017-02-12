/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2017/2/12 23:02:39                           */
/*==============================================================*/


drop table if exists Role;

drop table if exists RoleMenu;

drop table if exists SysMenu;

drop table if exists User;

drop table if exists UserRole;

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

alter table Role comment '用户角色表';

/*==============================================================*/
/* Table: RoleMenu                                              */
/*==============================================================*/
create table RoleMenu
(
   RoleMenuID           varchar(50) not null comment 'RoleMenuID',
   RoleID               varchar(50) not null comment 'RoleID',
   MenuID               varchar(50) not null comment 'MenuID',
   Remark               varchar(255) comment '备注',
   primary key (RoleMenuID)
);

alter table RoleMenu comment '角色菜单权限表';

/*==============================================================*/
/* Table: SysMenu                                               */
/*==============================================================*/
create table SysMenu
(
   MenuID               varchar(50) not null comment 'MenuID',
   MenuName             varchar(50) not null comment '菜单名称',
   MenuUrl              varchar(255) not null comment '菜单Url',
   MenuIcon             varchar(255) comment '菜单图标',
   MenuParentID         varchar(50) comment '菜单父级ID',
   MenuOrder            varchar(50) not null comment '菜单顺序',
   IsVisible            bool not null default false comment '是否显示菜单',
   primary key (MenuID)
);

alter table SysMenu comment '菜单权限表';

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

