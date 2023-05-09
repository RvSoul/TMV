echo off
echo "Press B to build images, P to push to registry, any other key to cancel"
set /p op= :
if "%op%"=="B" goto build
if "%op%"=="P" goto push
exit

:build
docker rmi tmv
docker build -f "Dockerfile" --force-rm -t tmv .
goto end

:push
docker tag tmv 124.221.218.182:8012/tmv/tmv
docker push 124.221.218.182:8012/tmv/tmv

goto end

:end
pause