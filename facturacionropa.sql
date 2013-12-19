-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 19-12-2013 a las 22:40:52
-- Versión del servidor: 5.5.32
-- Versión de PHP: 5.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `facturacionropa`
--
CREATE DATABASE IF NOT EXISTS `facturacionropa` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `facturacionropa`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `insumos`
--

CREATE TABLE IF NOT EXISTS `insumos` (
  `ins_id` int(11) NOT NULL AUTO_INCREMENT,
  `ins_desc` varchar(50) NOT NULL,
  `ins_cant` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ins_id`),
  UNIQUE KEY `ins_desc` (`ins_desc`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Volcado de datos para la tabla `insumos`
--

INSERT INTO `insumos` (`ins_id`, `ins_desc`, `ins_cant`) VALUES
(3, 'j', 213),
(6, '123', 213),
(7, 'etiqueta cuello', 100),
(8, 'etiqueta abajo', 100);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE IF NOT EXISTS `productos` (
  `prod_id` int(11) NOT NULL AUTO_INCREMENT,
  `prod_desc` varchar(100) NOT NULL,
  `prod_tasa_id` int(11) NOT NULL,
  PRIMARY KEY (`prod_id`),
  UNIQUE KEY `prod_desc` (`prod_desc`),
  KEY `prod_tasa_id` (`prod_tasa_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=33 ;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`prod_id`, `prod_desc`, `prod_tasa_id`) VALUES
(1, 'Pantalón Dama', 1),
(4, 'pantalon2', 1),
(14, 'hola', 1),
(17, 'hola2', 1),
(23, 'Buzo Varón', 1),
(27, 'NuevoProducto', 1),
(28, 'Short Vandalia', 1),
(29, 'Pantalón chupin', 1),
(30, 'joas', 1),
(31, '123', 1),
(32, 'Chango', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `prod_ins`
--

CREATE TABLE IF NOT EXISTS `prod_ins` (
  `prod_ins_prod_id` int(11) NOT NULL,
  `prod_ins_ins_id` int(11) NOT NULL,
  `prod_ins_cant` int(10) unsigned NOT NULL,
  PRIMARY KEY (`prod_ins_prod_id`,`prod_ins_ins_id`),
  KEY `prod_ins_ins_id` (`prod_ins_ins_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `prod_talles`
--

CREATE TABLE IF NOT EXISTS `prod_talles` (
  `prtll_prod_id` int(11) NOT NULL,
  `prtll_talle_id` int(11) NOT NULL,
  `prtll_precio_venta` decimal(10,2) NOT NULL,
  `prtll_renglon` tinyint(11) NOT NULL,
  PRIMARY KEY (`prtll_prod_id`,`prtll_talle_id`),
  KEY `prtll_talle_id` (`prtll_talle_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `prod_talles`
--

INSERT INTO `prod_talles` (`prtll_prod_id`, `prtll_talle_id`, `prtll_precio_venta`, `prtll_renglon`) VALUES
(1, 80, '83.60', 0),
(1, 81, '83.60', 0),
(1, 82, '83.60', 0),
(1, 84, '83.60', 0),
(1, 85, '84.60', 1),
(4, 67, '12.00', 0),
(4, 68, '12.00', 0),
(4, 69, '12.00', 0),
(4, 80, '20.00', 1),
(4, 81, '20.00', 1),
(4, 82, '60.00', 2),
(4, 83, '20.00', 1),
(4, 84, '60.00', 2),
(4, 85, '60.00', 2),
(14, 67, '1.00', 0),
(14, 68, '1.00', 0),
(14, 69, '1.00', 0),
(17, 67, '1.00', 0),
(17, 68, '1.00', 0),
(17, 69, '1.00', 0),
(23, 72, '53.30', 0),
(23, 74, '53.30', 0),
(23, 76, '53.30', 0),
(23, 77, '53.30', 0),
(23, 78, '53.30', 0),
(23, 80, '0.00', 1),
(23, 81, '0.00', 1),
(23, 82, '0.00', 1),
(23, 84, '0.00', 1),
(27, 67, '2.00', 0),
(27, 68, '2.00', 0),
(27, 80, '3.00', 1),
(27, 83, '3.00', 1),
(28, 74, '42.70', 0),
(28, 76, '42.70', 0),
(28, 77, '42.70', 0),
(28, 78, '45.60', 1),
(28, 79, '45.60', 1),
(28, 80, '45.60', 1),
(28, 81, '45.60', 1),
(28, 82, '45.60', 1),
(28, 83, '45.60', 1),
(28, 84, '45.60', 1),
(29, 68, '52.20', 0),
(29, 69, '52.20', 0),
(29, 70, '63.40', 1),
(29, 72, '63.40', 1),
(29, 74, '69.60', 2),
(29, 76, '69.60', 2),
(29, 77, '69.60', 2),
(29, 78, '83.60', 3),
(29, 79, '83.60', 3),
(29, 80, '83.60', 3),
(29, 81, '83.60', 3),
(29, 82, '83.60', 3),
(29, 83, '83.60', 3),
(29, 84, '83.60', 3),
(30, 68, '2.00', 0),
(30, 77, '2.00', 0),
(31, 77, '100.00', 0),
(32, 67, '78.00', 0),
(32, 68, '78.00', 0),
(32, 69, '78.00', 0),
(32, 70, '78.00', 0),
(32, 71, '78.00', 0),
(32, 72, '78.00', 0),
(32, 74, '78.00', 0),
(32, 80, '78.00', 0),
(32, 81, '78.00', 0),
(32, 83, '78.00', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `talles`
--

CREATE TABLE IF NOT EXISTS `talles` (
  `talle_id` int(11) NOT NULL AUTO_INCREMENT,
  `talle_descripcion` varchar(10) NOT NULL,
  PRIMARY KEY (`talle_id`),
  UNIQUE KEY `descripcion` (`talle_descripcion`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=86 ;

--
-- Volcado de datos para la tabla `talles`
--

INSERT INTO `talles` (`talle_id`, `talle_descripcion`) VALUES
(67, '1'),
(76, '10'),
(77, '12'),
(78, '14'),
(79, '16'),
(68, '2'),
(69, '3'),
(70, '4'),
(71, '5'),
(72, '6'),
(74, '8'),
(82, 'L'),
(81, 'M'),
(80, 'S'),
(84, 'XL'),
(83, 'XS'),
(85, 'XXL');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tasasiva`
--

CREATE TABLE IF NOT EXISTS `tasasiva` (
  `tasa_id` int(11) NOT NULL AUTO_INCREMENT,
  `tasa_desc` varchar(50) NOT NULL,
  `tasa_porciento` decimal(10,5) NOT NULL,
  PRIMARY KEY (`tasa_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Volcado de datos para la tabla `tasasiva`
--

INSERT INTO `tasasiva` (`tasa_id`, `tasa_desc`, `tasa_porciento`) VALUES
(0, 'Exento', '0.00000'),
(1, 'Tasa general', '0.21000'),
(2, 'Tasa diferencial', '0.27000'),
(3, 'Tasa bienes de uso', '0.10500');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE IF NOT EXISTS `usuarios` (
  `usu_id` varchar(20) NOT NULL,
  `usu_nombre` varchar(50) NOT NULL,
  `usu_contrasena` varchar(20) NOT NULL,
  `usu_per_id` int(3) NOT NULL,
  PRIMARY KEY (`usu_id`),
  KEY `usu_perfilid` (`usu_per_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`usu_id`, `usu_nombre`, `usu_contrasena`, `usu_per_id`) VALUES
('yamil', 'Yamil', 'yamil', 70);

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `productos`
--
ALTER TABLE `productos`
  ADD CONSTRAINT `productos_ibfk_1` FOREIGN KEY (`prod_tasa_id`) REFERENCES `tasasiva` (`tasa_id`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `prod_ins`
--
ALTER TABLE `prod_ins`
  ADD CONSTRAINT `prod_ins_ibfk_1` FOREIGN KEY (`prod_ins_prod_id`) REFERENCES `productos` (`prod_id`),
  ADD CONSTRAINT `prod_ins_ibfk_2` FOREIGN KEY (`prod_ins_ins_id`) REFERENCES `insumos` (`ins_id`);

--
-- Filtros para la tabla `prod_talles`
--
ALTER TABLE `prod_talles`
  ADD CONSTRAINT `prod_talles_ibfk_1` FOREIGN KEY (`prtll_prod_id`) REFERENCES `productos` (`prod_id`),
  ADD CONSTRAINT `prod_talles_ibfk_2` FOREIGN KEY (`prtll_talle_id`) REFERENCES `talles` (`talle_id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
