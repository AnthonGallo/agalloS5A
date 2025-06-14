﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agalloS5A.Model;
using SQLite;

namespace agalloS5A.Repository
{
    public class PersonRepository
    {
        String dbPath;
        private SQLiteConnection conn;
        public string statusMessage {  get; set; }

        public PersonRepository(String path)
        {
            dbPath = path;
        }

        private void Init()
        {
            if (conn is not null)
                return;
                conn = new(dbPath);
                conn.CreateTable<Persona>();
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");

                Persona persona = new() { Name = name };
                result = conn.Insert(persona);
                statusMessage = string.Format("Dato ingresado");
            }
            catch (Exception ex)
            {
                statusMessage = String.Format("Eror!" + ex);
            }
        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error!" + ex);
            }
            return new List<Persona>();
        }

        public void UpdatePerson(int id, string name)
        {
            try
            {
                Init();
                var persona = conn.Find<Persona>(id);
                if (persona == null)
                    throw new Exception("La persona no fue encuentrada");

                persona.Name = name;
                conn.Update(persona);
                statusMessage = "La persona se actualizó correctamente";
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
        }

        public void DeletePerson(int id) {
            try
            {
                Init();
                var persona = conn.Find<Persona>(id);
                if(persona == null)
                    throw new Exception("La persona no fue encontrada");

                conn.Delete(persona);
                statusMessage = "La persona se eliminó correctamente";
            }
            catch (Exception ex)
            {

                statusMessage = $"Error: {ex.Message}";
            }
        }
    }
}
