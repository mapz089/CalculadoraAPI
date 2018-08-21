# CalculatorAPI

#Inicio
Este proyecto expone algunos servicios para la realizació de operaciones matematicas como suma, resta, multiplicación y división.

#Prerequisites
El unico prerrequisito para correr este proyecto es tener visual studio 2015 o superiores instalado, ya que los servicios REST utilizan OWIN para ser autohospedados y no requerir IIS. Adicionalmente las referencias a ensamblados externos fue realizada mediante Nuget.

#Paso a paso
1. Abrir la solucion en visual studio.

2. Correr el proyecto por defecto los servicios estan expuestos en la url http://localhost:9000/
  *http://localhost:9000/api/calculator/add
    -body
      {
      "Addends" : [2,3,5,7]
      }
  *http://localhost:9000/api/calculator/sub
    -body
    {
    "Minuend" : 3,
    "Subtrahend": -4
    }
  *http://localhost:9000/api/calculator/mult
    -body
    {
    "Product": "32"
    }
  *http://localhost:9000/api/calculator/div
    -body
    {
    "Quotient": 11,
    "Remainder": 0
    }
  *http://localhost:9000/api/calculator/sqrt
    -body
    {
    "Square": 2
    }
  *http://localhost:9000/api/calculator/query
    -body
    {
    "Id": "ABC123"
    }
3.Consumir los servicios desde una aplicacion como Postman.
