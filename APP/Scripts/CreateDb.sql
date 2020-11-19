CREATE DATABASE IF NOT EXISTS pfswcho;
CREATE TABLE IF NOT EXISTS pfswcho.books
(
    id MEDIUMINT NOT NULL AUTO_INCREMENT,
    title VARCHAR(30),
    author VARCHAR(30),
    rate INT(3),
    PRIMARY KEY (id)
)