-- Adminer 4.8.1 MySQL 9.0.1 dump

SET NAMES utf8;
SET time_zone = '+00:00';
SET foreign_key_checks = 0;
SET sql_mode = 'NO_AUTO_VALUE_ON_ZERO';

DROP TABLE IF EXISTS `cliente`;
CREATE TABLE `cliente` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario` varchar(30) NOT NULL,
  `nivel` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `cliente` (`id`, `usuario`, `nivel`) VALUES
(1,	'carlos_martinez',	1),
(2,	'ana_garcia',	2),
(3,	'juan_perez',	3),
(4,	'maria_lopez',	4),
(5,	'luis_ramirez',	2),
(6,	'sofia_fernandez',	1),
(7,	'pedro_gonzalez',	4),
(8,	'laura_sanchez',	3),
(9,	'diego_torres',	2),
(10,	'elena_morales',	1);

SET NAMES utf8mb4;

DROP TABLE IF EXISTS `estado`;
CREATE TABLE `estado` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `estado` (`id`, `nombre`) VALUES
(1,	'Creado'),
(2,	'Pagado'),
(3,	'Preparado'),
(4,	'Entregado'),
(5,	'Cancelado');

DROP TABLE IF EXISTS `pedidos`;
CREATE TABLE `pedidos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `cliente_id` int NOT NULL,
  `pizza_id` int NOT NULL,
  `cantidad` int NOT NULL,
  `total` decimal(10,2) NOT NULL,
  `estado` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


DROP TABLE IF EXISTS `pizza`;
CREATE TABLE `pizza` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `pizza` (`id`, `nombre`, `precio`) VALUES
(1,	'Pizza Margarita',	8.99),
(2,	'Pizza Pepperoni',	10.50),
(3,	'Pizza Cuatro Quesos',	11.75),
(4,	'Pizza Hawaiana',	9.99),
(5,	'Pizza Vegetariana',	9.50),
(6,	'Pizza BBQ Pollo',	12.25),
(7,	'Pizza Mexicana',	11.00),
(8,	'Pizza de Jamón',	9.00),
(9,	'Pizza Carbonara',	13.50),
(10,	'Pizza Napolitana',	10.75);

-- 2024-11-06 05:00:04
