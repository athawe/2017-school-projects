USE demo;

CREATE TABLE surveyData (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL,
  age int(10) NOT NULL,
  status varchar(50) NOT NULL,
  howPurchased varchar(25) NOT NULL,
  purchase1 varchar(25) NOT NULL,
  satisfaction1 int(10) NOT NULL,
  recommend1 varchar(25) NOT NULL,
  purchase2 varchar(25) NULL,
  satisfaction2 int(10) NULL,
  recommend2 varchar(25) NULL,
  purchase3 varchar(25) NULL,
  satisfaction3 int(10) NULL,
  recommend3 varchar(25) NULL,
  purchase4 varchar(25) NULL,
  satisfaction4 int(10) NULL,
  recommend4 varchar(25) NULL,
  purchase5 varchar(25) NULL,
  satisfaction5 int(10) NULL,
  recommend5 varchar(25) NULL,
  PRIMARY KEY (id)
);
