# Ecletica Beer Control (Api)
Aplicação desenvolvida em .NET para gerenciamento/controle de dispositivos (Ecletica Beer Devices) 

## Deploy

Os workflows deste repositorio delegam build, push de imagem e deploy Kubernetes para:

```text
Autus-Solutions/autus-infra
```

A fonte de verdade da infraestrutura compartilhada da Autus fica no repositorio `autus-infra`.

Os manifests legados que apontavam para o registry local do MicroK8s foram removidos deste branch de migracao para evitar deploys fora do padrao Autus.

Secrets de aplicacao devem existir no Kubernetes como:

```text
ecletica-beer-control-api-secrets
ecletica-beer-control-worker-secrets
ecletica-beer-control-mcp-secrets
```
