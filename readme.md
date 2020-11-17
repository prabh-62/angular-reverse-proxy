# Starting development

## Server - Reverse Proxy

Requirements

```bash
❯ dotnet --version
5.0.100
```

```bash
❯ cd Frontdoor
❯ dotnet dev-certs https --trust
❯ dotnet run
```

## Client - Angular

Requirements

```bash
❯ node --version
v15.2.0
❯ yarn --version
1.22.10
```

```bash
❯ cd web-application
❯ yarn install
❯ yarn start:support
❯ yarn start:admin
```

Navigate to [https://localhost/support/](https://localhost/support/) or [https://localhost/admin/](https://localhost/admin/) for getting started with development
