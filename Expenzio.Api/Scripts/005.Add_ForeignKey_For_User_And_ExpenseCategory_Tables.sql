-- using PostgresQL

ALTER TABLE expense_category
ADD COLUMN user_id UUID REFERENCES expenzio_user(id);
