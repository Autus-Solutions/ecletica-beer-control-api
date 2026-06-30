# Ecletica Beer Control (Api)
Aplicação desenvolvida em .NET para gerenciamento/controle de dispositivos (Ecletica Beer Devices) 

## Deploy

Os workflows deste repositorio delegam build, push de imagem e deploy Kubernetes para:

```text
Autus-Solutions/autus-infra
```

Os manifests em `.github/deployments` sao legado operacional e devem ser usados apenas como referencia ou rollback. A fonte de verdade da infraestrutura compartilhada da Autus fica no repositorio `autus-infra`.

Secrets de aplicacao devem existir no Kubernetes como:

```text
ecletica-beer-control-api-secrets
ecletica-beer-control-worker-secrets
ecletica-beer-control-mcp-secrets
```
