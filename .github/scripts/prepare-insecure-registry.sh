echo "Cleaning docker daemon.json ..."
sudo rm -rf /etc/docker/daemon.json
echo "Preparing ..."
sudo bash -c 'echo "{\"insecure-registries\" : [\"49.13.136.239:32000\"]}" >> /etc/docker/daemon.json'
echo "Restarting docker ..."
sudo systemctl restart docker
echo "Finished!"