/*
Created: 30.05.2012
Modified: 30.05.2012
Model: hcdb
Database: MySQL 5.0
*/

-- Drop tables section ---------------------------------------------------

ALTER TABLE Analis DROP FOREIGN KEY Relationship4
;
ALTER TABLE Healing DROP FOREIGN KEY Relationship3
;
ALTER TABLE Healing DROP FOREIGN KEY Relationship1
;
ALTER TABLE Doctor DROP FOREIGN KEY Relationship5
;
ALTER TABLE user DROP FOREIGN KEY Relationship6
;
ALTER TABLE Doctor DROP FOREIGN KEY Relationship7
;
ALTER TABLE Patient DROP FOREIGN KEY Relationship8
;
ALTER TABLE spec DROP FOREIGN KEY Relationship9
;

ALTER TABLE Doctor DROP KEY Key1
;
ALTER TABLE Patient DROP KEY Key2
;

DROP TABLE Doctor
;
DROP TABLE Healing
;
DROP TABLE Patient
;
DROP TABLE Analis
;
DROP TABLE Spec
;
DROP TABLE User
;
DROP TABLE User_type
;
DROP TABLE Department
;

-- Create tables section -------------------------------------------------

-- Table Doctor

CREATE TABLE Doctor (
    id_doctor Int NOT NULL AUTO_INCREMENT,
    id_user Int NOT NULL,
    id_spec Int NOT NULL,
    FIO Varchar(100),
    room Int,
    PRIMARY KEY (id_doctor)
)
;

ALTER TABLE Doctor ADD UNIQUE Key1 (id_user)
;

-- Table Healing

CREATE TABLE Healing (
    id_healing Int NOT NULL AUTO_INCREMENT,
    id_doctor Int NOT NULL,
    id_patient Int NOT NULL,
    comments Varchar(1000),
    diagnosis Varchar(500),
    drugs Varchar(100),
    healing_time Datetime NOT NULL,
    PRIMARY KEY (id_healing)
)
;

-- Table Patient

CREATE TABLE Patient (
    id_patient Int NOT NULL AUTO_INCREMENT,
    id_user Int NOT NULL,
    FIO Varchar(100),
    Adress Varchar(100),
    Safety_card Int,
    tel Varchar(25),
    PRIMARY KEY (id_patient)
)
;

ALTER TABLE Patient ADD UNIQUE Key2 (id_user)
;

-- Table Analis

CREATE TABLE Analis (
    id_analis Int NOT NULL AUTO_INCREMENT,
    id_patient Int NOT NULL,
    type Varchar(100),
    conclusion Varchar(500),
    PRIMARY KEY (id_analis)
)
;

CREATE TABLE department (
    id_department Int NOT NULL,
    name Varchar(50) NOT NULL,
    PRIMARY KEY (id_department)
)
;

CREATE TABLE spec (
    id_spec Int NOT NULL,
    id_department Int NOT NULL,
    name Varchar(50) NOT NULL,
    PRIMARY KEY (id_spec)
)
;

CREATE TABLE user (
    id_user Int NOT NULL AUTO_INCREMENT,
    id_user_type Int NOT NULL,
    login Varchar(100) NOT NULL,
    password Varchar(100) NOT NULL,
    PRIMARY KEY (id_user)
)
;

CREATE TABLE user_type (
    id_user_type Int NOT NULL,
    name Varchar(50) NOT NULL,
    PRIMARY KEY (id_user_type)
)
;

-- Create relationships section ------------------------------------------------- 

ALTER TABLE Healing ADD CONSTRAINT Relationship1 FOREIGN KEY (id_doctor) REFERENCES Doctor (id_doctor) ON DELETE NO ACTION ON UPDATE NO ACTION
;

ALTER TABLE Healing ADD CONSTRAINT Relationship3 FOREIGN KEY (id_patient) REFERENCES Patient (id_patient) ON DELETE NO ACTION ON UPDATE NO ACTION
;

ALTER TABLE Analis ADD CONSTRAINT Relationship4 FOREIGN KEY (id_patient) REFERENCES Patient (id_patient) ON DELETE NO ACTION ON UPDATE NO ACTION
;

ALTER TABLE Doctor ADD CONSTRAINT Relationship5 FOREIGN KEY (id_spec) REFERENCES spec (id_spec) ON DELETE NO ACTION ON UPDATE NO ACTION
;

ALTER TABLE user ADD CONSTRAINT Relationship6 FOREIGN KEY (id_user_type) REFERENCES user_type (id_user_type) ON DELETE NO ACTION ON UPDATE NO ACTION
;

ALTER TABLE Doctor ADD CONSTRAINT Relationship7 FOREIGN KEY (id_user) REFERENCES user (id_user) ON DELETE NO ACTION ON UPDATE NO ACTION
;

ALTER TABLE Patient ADD CONSTRAINT Relationship8 FOREIGN KEY (id_user) REFERENCES user (id_user) ON DELETE NO ACTION ON UPDATE NO ACTION
;

ALTER TABLE spec ADD CONSTRAINT Relationship9 FOREIGN KEY (id_department) REFERENCES department (id_department) ON DELETE NO ACTION ON UPDATE NO ACTION
;

INSERT INTO department (id_department,name) VALUES (0, 'Hirurgicheskoe')
;

INSERT INTO department (id_department,name) VALUES (1, 'manual diagnostis')
;

INSERT INTO spec (id_spec, name, id_department) VALUES (0, 'Hirurg', 0)
;

INSERT INTO spec (id_spec, name, id_department) VALUES (1, 'Okulist', 1)
;

INSERT INTO spec (id_spec, name, id_department) VALUES (2, 'Ottolaringolog', 1)
;

INSERT INTO user_type (id_user_type, name) VALUES (0, 'Registrator')
;

INSERT INTO user_type (id_user_type, name) VALUES (1, 'Doctor')
;

INSERT INTO user_type (id_user_type, name) VALUES (2, 'User')
;

INSERT INTO user (id_user_type, login, password) VALUES (0, 'morfius', 'smittdie')
;

INSERT INTO user (id_user_type, login, password) VALUES (1, 'house', 'ilikeppl')
;

INSERT INTO user (id_user_type, login, password) VALUES (1, 'cox', 'ilikeplp2')
;

INSERT INTO user (id_user_type, login, password) VALUES (1, 'bormental', '80kg')
;

INSERT INTO user (id_user_type, login, password) VALUES (2, 'babadusya', 'vecherinki2')
;

INSERT INTO user (id_user_type, login, password) VALUES (2, 'guf', '7flor')
;

INSERT INTO doctor (id_user, id_spec, FIO, room) VALUES (2, 0, 'Gregory House', 13)
;

INSERT INTO doctor (id_user, id_spec, FIO, room) VALUES (3, 1, 'Perry Cox', 14)
;

INSERT INTO doctor (id_user, id_spec, FIO, room) VALUES (4, 2, 'Doctor Bobmental', 15)
;

INSERT INTO patient (id_user, FIO, Adress, Safety_card, tel) VALUES (5, 'Evdokia Aleksandrovna Geroin', 'Novoibiza d.5 kv.1408', 132353, 'nokia lumia 900')
;

INSERT INTO patient (id_user, FIO, Adress, Safety_card, tel) VALUES (6, 'Aleksandr GUF Dolmatov', 'Heaven/Hell', 132452, '2978473')
;

INSERT INTO healing (id_doctor, id_patient, healing_time) VALUES (1, 1, '2012-06-04 9:00:00')
;