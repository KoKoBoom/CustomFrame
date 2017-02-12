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
   RoleName             varchar(255) not null comment '��ɫ����',
   RoleRemark           varchar(255) comment '��ɫ��ע
            ',
   primary key (RoleID)
);

alter table Role comment '�û���ɫ��';

/*==============================================================*/
/* Table: RoleMenu                                              */
/*==============================================================*/
create table RoleMenu
(
   RoleMenuID           varchar(50) not null comment 'RoleMenuID',
   RoleID               varchar(50) not null comment 'RoleID',
   MenuID               varchar(50) not null comment 'MenuID',
   Remark               varchar(255) comment '��ע',
   primary key (RoleMenuID)
);

alter table RoleMenu comment '��ɫ�˵�Ȩ�ޱ�';

/*==============================================================*/
/* Table: SysMenu                                               */
/*==============================================================*/
create table SysMenu
(
   MenuID               varchar(50) not null comment 'MenuID',
   MenuName             varchar(50) not null comment '�˵�����',
   MenuUrl              varchar(255) not null comment '�˵�Url',
   MenuIcon             varchar(255) comment '�˵�ͼ��',
   MenuParentID         varchar(50) comment '�˵�����ID',
   MenuOrder            varchar(50) not null comment '�˵�˳��',
   IsVisible            bool not null default false comment '�Ƿ���ʾ�˵�',
   primary key (MenuID)
);

alter table SysMenu comment '�˵�Ȩ�ޱ�';

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

