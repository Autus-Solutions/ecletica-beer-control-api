echo "Cleaning docker daemon.json ..."
sudo rm -rf /etc/docker/daemon.json
echo "Preparing ..."
sudo bash -c 'echo "{\"insecure-registries\" : [\"5.161.202.205:32000\"]}" >> /etc/docker/daemon.json'
echo "Restarting docker ..."
sudo systemctl restart docker
echo "Finished!"