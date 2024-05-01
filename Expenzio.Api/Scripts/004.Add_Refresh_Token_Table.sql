-- using PostgresQL

CREATE TABLE IF NOT EXISTS refresh_token (
    token VARCHAR(256) NOT NULL PRIMARY KEY,
    user_id UUID REFERENCES expenzio_user(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    valid_until TIMESTAMP NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE
);
