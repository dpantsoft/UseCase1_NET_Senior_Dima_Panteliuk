# Use Case #1 - .NET - Senior - Dima Panteliuk

## Application Description
CountryList is a web application designed to provide users with a comprehensive list of countries. With its powerful filters, users can search for countries based on certain criteria such as population, name, and more. The application interfaces with an external RESTful Countries API, bringing together a seamless combination of data extraction and user experience. Beyond just displaying countries, the application offers pagination, sorting, and filtering functionalities, ensuring that users can narrow down to the precise list they're seeking with minimal effort.

Designed with simplicity and efficiency in mind, CountryList is the go-to application for those who wish to explore or learn more about the nations of the world. By leveraging the potential of ASP.NET Core, the application stands out in terms of performance, scalability, and ease of use.

## Running the Application Locally

1. Ensure .NET Core SDK is set up on your computer.
2. Clone the repository or download the source files.
3. Transition to the project's root directory using your terminal or command prompt.
4. To restore the project dependencies, input the command:
   ```
   dotnet restore
   ```
5. To kickstart the application, type:
   ```
   dotnet run
   ```
6. After activation, access the application by directing to `http://localhost:5159` or the port showcased in the terminal.


## Using the Developed Endpoint

The primary endpoint designed for procuring countries is `GetCountries`. It accommodates multiple query parameters for concise filtering, orderly sorting, and straightforward pagination.

Below are 10 instances illustrating the utilization of the `GetCountries` endpoint:

1. Retrieve the initial 10 countries (standard operation):
   ```
   GET http://localhost:5159/api/Countries/GetCountries
   ```

2. Search for countries featuring the name "India":
   ```
   GET http://localhost:5159/api/Countries/GetCountries?searchString=India
   ```

3. Fetch countries possessing a population below 1 million:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?populationLessThan=1000000
   ```

4. Obtain the second page of countries, using the default 10 countries per page:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?pageNumber=2
   ```

5. Retrieve 20 countries per page:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?pageSize=20
   ```

6. Arrange countries by name:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?sortAttribute=name
   ```

7. Filter countries containing "land" and then sequence by name:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?searchString=land&sortAttribute=name
   ```

8. Source countries with populations under 500,000, ordered by name:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?populationLessThan=500000&sortAttribute=name
   ```

9. Access the third set of countries, taking 5 countries per page:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?pageNumber=3&pageSize=5
   ```

10. Fuse multiple filters: isolate countries incorporating "United" in the name, with populations under 50 million, in name sequence, and acquire the second page showing 15 countries:
   ```
   GET http://localhost:5159/api/Countries/GetCountries?searchString=United&populationLessThan=50000000&sortAttribute=name&pageNumber=2&pageSize=15
   ```

## Swagger Endpoint

For comprehensive API documentation, you can utilize the Swagger UI by navigating to:
[http://localhost:5159/swagger](http://localhost:5159/swagger)

Swagger provides an interactive way to visualize and test your API's endpoints. Make sure to explore this tool to better understand the capabilities and functionalities of the CountryList app.