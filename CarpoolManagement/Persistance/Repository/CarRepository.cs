﻿using CarpoolManagement.Source.Models;

namespace CarpoolManagement.Persistance.Repository
{
    public class CarRepository
    {
        private readonly Dictionary<string, Car> _cars;

        private readonly List<Car> _carList = new()
        {
            new Car() { Plate = "AB 123-CD", Name = "Blue Beetle - Commute Transport", Type = "VW Beetle", Color = Color.Blue,  NumberOfSeats = 4},
            new Car() { Plate = "CD 456-EF", Name = "Mustang - Quick support", Type = "Ford Mustang", Color = Color.Gray,  NumberOfSeats = 4},
            new Car() { Plate = "EF 789-GH", Name = "Octavia - Travel", Type = "Skoda Octavia", Color = Color.Black,  NumberOfSeats = 5},
            new Car() { Plate = "GH 123-IJ", Name = "Carnival - Team Travel", Type = "Kia Carnival", Color = Color.Red,  NumberOfSeats = 7},
            new Car() { Plate = "IJ 456-KL", Name = "Tacoma - Off Road Travel", Type = "Toyota Tacoma", Color = Color.Green,  NumberOfSeats = 4},
            new Car() { Plate = "KL 789-MN", Name = "Fabia #1 - Basic Travel", Type = "Skoda Fabia", Color = Color.White,  NumberOfSeats = 5},
            new Car() { Plate = "MN 123-OP", Name = "Fabia #2 - Basic Travel", Type = "Skoda Fabia", Color = Color.White,  NumberOfSeats = 5},
            new Car() { Plate = "OP 465-QR", Name = "Fabia #3 - Basic Travel", Type = "Skoda Fabia", Color = Color.White,  NumberOfSeats = 5},
            new Car() { Plate = "QR 789-ST", Name = "Camaro - Quick support", Type = "Chevrolet Camaro", Color = Color.Yellow,  NumberOfSeats = 4},
            new Car() { Plate = "ST 123-UV", Name = "Bus - Interurban transport", Type = "Iveco Crossway", Color = Color.Other,  NumberOfSeats = 63},
        };

        public CarRepository()
        {
            _cars = _carList.ToDictionary(keySelector: car => car.Plate, elementSelector: car => car);
        }

        public IEnumerable<Car> GetAll() => _cars.Values.ToList();
        public Car? GetByPlate(string plate)
        {
            _cars.TryGetValue(plate, out Car? car);
            return car;
        }
    }
}
