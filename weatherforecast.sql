CREATE TABLE IF NOT EXISTS role_person (
    id_role_person SERIAL PRIMARY KEY,
    nama_role VARCHAR(100) NOT NULL,
	created_at DEFAULT CURRENT_TIMESTAMP,
	updated_at DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS status (
    id_status SERIAL PRIMARY KEY,
    status VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS person (
    id_person SERIAL PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    alamat TEXT,
    email VARCHAR(100),
    password TEXT NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    id_role_person INT,
    id_status INT DEFAULT 1,

    CONSTRAINT fk_role
        FOREIGN KEY (id_role_person) REFERENCES role_person(id_role_person),

    CONSTRAINT fk_status
        FOREIGN KEY (id_status) REFERENCES status(id_status)
);

CREATE TABLE IF NOT EXISTS weather_forecast (
    id SERIAL PRIMARY KEY,
    date DATE NOT NULL,
    temperature_c INT NOT NULL,
    temperature_f INT GENERATED ALWAYS AS (32 + (temperature_c / 0.5556)) STORED,
    summary VARCHAR(255),

    id_person INT,

    CONSTRAINT fk_person_weather
        FOREIGN KEY (id_person) REFERENCES person(id_person)
);

INSERT INTO role_person (nama_role) VALUES
('admin'),
('user');

INSERT INTO status (status) VALUES
('aktif'),
('deleted');

INSERT INTO person (nama, alamat, email, password, id_role_person)
VALUES 
('Super Admin', 'Jakarta', 'admin@email.com', 'superadmin', 1),
('Admin Utama', 'Surabaya', 'admin@gmail.com', 'admin123', 1),
('User Test 1', 'Malang', 'user1@gmail.com', 'user123', 2),
('User Test 2', 'Jakarta', 'user2@gmail.com', 'user123', 2),
('User Test 3', 'Bandung', 'user3@gmail.com', 'user123', 2);