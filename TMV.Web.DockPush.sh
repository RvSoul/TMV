docker stop tmvrq
docker rmi 124.221.218.182:8012/tmv/tmv
docker pull  124.221.218.182:8012/tmv/tmv
docker run --name=tmvrq -d -p 5000:5000 -p 5395:5395 -d 124.221.218.182:8012/tmv/tmv