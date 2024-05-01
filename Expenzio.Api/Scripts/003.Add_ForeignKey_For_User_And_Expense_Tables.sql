-- using PostgresQL
ALTER TABLE expense
ADD COLUMN user_id UUID REFERENCES expenzio_user(id);
