# .NET Backend Blueprint
A comprehensive .NET 10 backend template using Aspire for local development and deployment to Azure. This template provides a modern, cloud-ready API with authentication, authorization and database integration.

**Video Walkthrough**: [Watch the full tutorial on YouTube](https://youtu.be/EfKC_9I1YiM)

## Overview

This template includes:

- **.NET 10 Web API** with Entity Framework Core and PostgreSQL
- **Vertical Slice Architecture** with feature-based organization
- **Keycloak authentication** for JWT-based security
- **Global error handling** for consistent API responses
- **Aspire** orchestration for local development
- **Azure Container Apps** deployment ready
- **CI/CD pipeline** for GitHub Actions

## Prerequisites

Before getting started, ensure you have the following tools installed:

- **[.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)** - The latest .NET SDK
- **[Docker Desktop](https://www.docker.com/products/docker-desktop/)** - For containerized services (PostgreSQL, Keycloak)
- **[Aspire CLI](https://learn.microsoft.com/dotnet/aspire/cli/install)** - To run the application locally and deploy to Azure

## Getting Started

### Running the Project Locally

1. **Navigate to the root directory** of the project
2. **Run the application** using Aspire:
   ```bash
   dotnet run --project src/inventory.AppHost
   ```
   Or alternatively:
   ```bash
   aspire run
   ```

3. **Access the Aspire Dashboard** using the URL provided in the terminal output

### Testing the API

Once the application is running, you can test the API using:

- **Swagger UI**: Access it from the Aspire Dashboard by clicking the **API Docs** link for the API service
- **Aspire Dashboard**: Monitor application health, logs, and metrics through the dashboard

### Authentication with Keycloak

The template uses Keycloak for authentication. Once the application is running:

#### Testing API with Swagger UI

1. **Access Swagger UI** from the Aspire Dashboard by clicking the **API Docs** link
2. **Click the "Authorize" button** in the Swagger UI interface
3. **Complete the OAuth2 flow**:
   - You'll be redirected to the Keycloak login page
   - **Login with**: `demo` / `demo`
   - After successful authentication, you'll be redirected back to Swagger UI
4. **Test API endpoints**: You can now use all API endpoints in Swagger UI with the obtained access token

#### Managing Keycloak (Admin Access)

If you need to manage Keycloak realm, clients, scopes, or users:

1. **Access Keycloak Admin Console** from the Aspire Dashboard by clicking on the Keycloak service endpoint
2. **Login with admin credentials**:
   - **Username**: `admin`
   - **Password**: `admin`
3. From here you can:
   - Create additional users
   - Configure client scopes
   - Manage roles and permissions
   - Adjust authentication flows

#### Exporting Keycloak Realm

To export the Keycloak realm configuration (useful for backup or version control):

1. **Find the Keycloak volume name** assigned by Aspire:
   ```bash
   docker volume ls
   ```
   Look for a volume name similar to `inventory.apphost-<hash>-keycloak-data`

2. **Stop the running Keycloak container** (if any):
   ```bash
   docker ps
   ```
   Find the Keycloak container ID, then stop it:
   ```bash
   docker stop <keycloak-container-id>
   ```

3. **Run the export command** (replace `<keycloak-volume-name>` with the actual volume name):
   ```bash
   docker run --rm -v <keycloak-volume-name>:/opt/keycloak/data -v "${PWD}\kc-export:/export" -e KC_DB=dev-file quay.io/keycloak/keycloak:26.4 export --realm inventory --dir /export --users realm_file
   ```

4. The exported realm configuration will be saved to the `kc-export` directory in your project root

## Azure Deployment

### Deploy with Aspire

1. **Deploy to Azure**:
   ```bash
   aspire deploy
   ```

2. Follow the prompts to:
   - Select your Azure subscription
   - Choose a deployment region
   - Provide environment-specific configuration

The deployment will provision:
- Azure Container Apps Environment
- Azure Container Apps for the API backend
- Azure Container Apps for Keycloak
- PostgreSQL Flexible Server
- Container Registry
- Managed Identity
- All necessary networking and security configurations

### Post-Deployment Configuration

After the deployment completes, you need to configure Keycloak for your Azure environment:

#### 1. Import the Realm Configuration

1. **Access your deployed Keycloak instance** using the URL provided in the Azure Container Apps
2. **Login to the Admin Console**:
   - **Username**: `admin`
   - **Password**: Check your Azure Key Vault or deployment outputs for the admin password
3. **Import the realm**:
   - Navigate to the realm dropdown in the top-left corner
   - Click **Create Realm**
   - Click **Browse** and select `src/inventory.AppHost/realms/inventory-realm.json`
   - Click **Create** to import the realm

### Testing the API with Postman

A Postman collection is included at [src/inventory.Api/postman_collection.json](src/inventory.Api/postman_collection.json) for testing the API, both locally and on Azure.

#### Setting up Postman

1. **Import the collection**:
   - Open Postman
   - Click **Import** and select `src/inventory.Api/postman_collection.json`

2. **Configure collection variables**:
   - Right-click the imported collection and select **Edit**
   - Navigate to the **Variables** tab
   - For **local testing**, the default values should work (check the current values)
   - For **Azure testing**, update:
     - `baseUrl` with your deployed API endpoint (e.g., `https://your-api.azurecontainerapps.io`)
     - `keycloakUrl` with your deployed Keycloak endpoint (e.g., `https://your-keycloak.azurecontainerapps.io`)
   - Save the changes

3. **Authenticate for protected endpoints** (POST/PUT/DELETE requests):
   - Navigate to the collection's **Authorization** tab
   - Click **Get New Access Token** (OAuth 2.0 settings are pre-configured)
   - Sign in on the Keycloak page with **demo** / **demo**
   - Click **Use Token** to apply it to your requests

You can now send requests to test your deployed API!

## CI/CD Pipelines (Optional)

### GitHub Actions

While `aspire deploy` is great for manual deployments, you can set up automated CI/CD pipelines for continuous deployment.

**Note**: The template includes a basic Azure Developer CLI configuration. You may need to adapt the GitHub Actions workflow to work with Aspire deployment commands or continue using the existing azd-based workflow.

To set up GitHub Actions with Azure Developer CLI:

1. **Install Azure Developer CLI** if not already installed:
   ```bash
   winget install microsoft.azd
   ```

2. **Initialize the Azure Developer CLI project**:
   ```bash
   azd init
   ```

3. **Run the pipeline configuration command**:
   ```bash
   azd pipeline config --provider github
   ```

4. **Follow the interactive prompts** to configure:
   - GitHub repository connection
   - Azure authentication (Federated Identity recommended)
   - Deployment settings and environments

4. **Commit and push** your changes to trigger the pipeline

## Project Structure

```
├── src/
│   ├── inventory.Api/              # Main Web API project
│   │   ├── Features/                 # Feature-based organization (Vertical Slice Architecture)
│   │   ├── Data/                     # Entity Framework context and configurations
│   │   ├── Models/                   # Domain models
│   │   └── Shared/                   # Shared components (auth, CORS, error handling, etc.)
│   ├── inventory.AppHost/          # Aspire orchestration
│   └── inventory.ServiceDefaults/  # Shared service configurations
├── .github/workflows/                # GitHub Actions workflows
├── .devcontainer/                    # Dev container configuration
└── azure.yaml                       # Azure Developer CLI configuration
```

## Architecture

This template follows **Vertical Slice Architecture** principles, organizing code by features rather than technical layers. The architecture is structured as follows:

### Feature Organization

Each feature (like `Items` or `Categories`) in the `Features/` folder contains:

- **Feature Endpoints** - A main endpoints class that groups and maps all related routes
- **Individual Operations** - Each operation (GetItems, CreateItem, UpdateItem, etc.) has its own folder containing:
  - **Endpoint** - The minimal API endpoint definition with all business logic inline
  - **DTOs** - Request/response models specific to that operation

### Example Structure

```
Features/
├── Items/
│   ├── ItemsEndpoints.cs           # Groups all item-related endpoints
│   ├── Constants/                  # Shared constants for the feature
│   ├── CreateItem/
│   │   ├── CreateItemEndpoint.cs   # POST endpoint with business logic
│   │   └── CreateItemDtos.cs       # Request/response DTOs
│   ├── GetItems/
│   │   ├── GetItemsEndpoint.cs     # GET collection endpoint
│   │   └── GetItemsDtos.cs         # DTOs for pagination and filtering
│   └── GetItem/
│       ├── GetItemEndpoint.cs      # GET single item endpoint
│       └── GetItemDtos.cs          # Single item response DTOs
```

### Key Principles

This approach promotes:
- **Self-contained operations** - Each endpoint contains its complete logic flow
- **Feature cohesion** - All related operations are grouped together
- **Minimal dependencies** - Each operation only depends on what it needs
- **Easy testing** - Individual operations can be tested in isolation
- **Simple maintenance** - Changes to one operation don't affect others

## Installing the Template

To use this template for creating new projects:

1. **Navigate to the template root directory**
2. **Install the template**:
   ```bash
   dotnet new install .\
   ```

### Using the Template

Once installed, create a new project using the template:

```bash
# Create a new project in the current directory
dotnet new backend

# Create a new project with a specific name
dotnet new backend -n MyAwesomeBackend
```

### Template Parameters

The template supports the following parameters:

- `-n|--name`: Name of the project (default: current directory name)


## Configuration

### Local Development

The template uses Keycloak for local authentication. Configuration is handled automatically through Aspire service discovery.

**Key features enabled for local development:**
- **Global error handling** - Consistent error responses across all endpoints
- **CORS configuration** - Properly configured for local development
- **Health checks** - Built-in health monitoring endpoints
- **Logging and telemetry** - Integrated with Aspire dashboard

### Production

In production (Azure), the following are automatically configured:
- Managed Identity for secure service-to-service communication
- Azure Database for PostgreSQL
- Container Apps for scalable hosting


## License

This template is provided as-is for educational and development purposes.