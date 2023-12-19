CREATE TABLE students
(
    id            serial PRIMARY KEY,
    student_name  text NOT NULL,
    study_group   text NOT NULL,
    date_of_birth date NOT NULL
);

COPY students (id, student_name, study_group, date_of_birth)
    FROM '/data/database/students.csv'
    DELIMITER ','
    CSV HEADER;