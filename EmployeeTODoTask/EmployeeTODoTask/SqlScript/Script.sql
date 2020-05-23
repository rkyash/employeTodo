CREATE TABLE emp_record
(
	    id bigserial,
		emp_name varchar(300),
	
    CONSTRAINT emp_record_pkey PRIMARY KEY (id)
		 
   
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
ALTER TABLE emp_record OWNER to testuser;
CREATE INDEX idx_emp_record_emp_name ON emp_record USING btree (lower(emp_name)) TABLESPACE pg_default;



----------------------------------------------------------


CREATE TABLE emp_task
(
	    id bigserial,
		emp_id bigint,
		title varchar(300),	
		description text,
		prioritylevel_id bigint,
		state_id bigint,
		estimate varchar(1000),
	
    CONSTRAINT emp_task_pkey PRIMARY KEY (id)
		 
   
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
ALTER TABLE emp_task OWNER to testuser;
CREATE INDEX idx_emp_task_prioritylevel_id ON emp_task USING btree (prioritylevel_id) TABLESPACE pg_default;
CREATE INDEX idx_emp_task_emp_id ON emp_task USING btree (emp_id) TABLESPACE pg_default;
CREATE INDEX idx_emp_task_state_id ON emp_task USING btree (state_id) TABLESPACE pg_default;

---------------------------------------------------------------------------

CREATE TABLE priority_level
(
	    id bigserial,
		priority_level varchar(100),
	
    CONSTRAINT priority_level_pkey PRIMARY KEY (id)
		 
   
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
ALTER TABLE priority_level OWNER to testuser;


INSERT INTO priority_level (priority_level) values ('Critical'),('Medium'),('Low');
-------------------------------------------------------------------


CREATE TABLE statetbl
(
	    id bigserial,
		state_name varchar(100),
	
    CONSTRAINT statetbl_pkey PRIMARY KEY (id)
		 
   
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
ALTER TABLE statetbl OWNER to testuser;


INSERT INTO statetbl (state_name) values ('New'),('Active'),('Resolved'),('Closed');


