# Starting development

## Server - Reverse Proxy

Requirements

```bash
❯ dotnet --version
3.1.403
```

```bash
❯ cd Frontdoor
❯ dotnet dev-certs https --trust
❯ sudo dotnet run #(or just dotnet run if on windows)
```

## Client - Angular

Requirements

```bash
❯ node --version
v15.0.1
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
