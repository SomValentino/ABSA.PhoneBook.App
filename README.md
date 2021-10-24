# ABSA.PhoneBook.App

The solution is broken down into two the backend and the frontend. Both contain either contain single or multiple projects in which a break down is given below:

# Backend

The Backend is made up of three projects namely:

1. ABSA.PhoneBook.API:  This is the API projects

2. ABSA.PhoneBook.Data: The data access layer project

3. ABSA.PhoneBook.Domain: The domain class project.

# Frontend

1. ABSA.PhoneBook.WebApp: The folder that contains the react.js project 

# Running Project with Docker-Compose

The docker support has been added to the projects. Containers have been built for sql server database, API and react frontend.

To run the solution with docker-compose kindly run the following code below in the root of the solution. i.e the folder that contains the solution file:

```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

```
If running on visual studio select docker-compose as the startup project.

Once the docker containers have been started.

API Url : http://localhost:5001/swagger

React Frontend: http://localhost:3005


