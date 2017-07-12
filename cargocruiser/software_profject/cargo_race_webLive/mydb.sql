CREATE DATABASE cargoraceDB;
use cargoraceDB;


CREATE TABLE `scores` (
   `id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
   `name` VARCHAR(15) NOT NULL DEFAULT 'anonymous',
   `score` INT(10) UNSIGNED NOT NULL DEFAULT '0'
);


INSERT INTO scores (ID,`NAME`,score)
VALUES (1, 'Bob',5000),
	   (2, 'Bill',5600),
       (3, 'jfk',7200);