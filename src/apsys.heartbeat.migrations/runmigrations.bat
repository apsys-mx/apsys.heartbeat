echo off
cd bin/Debug/net6.0
apsys.heartbeat.migrations /cnn:"Server=.;Database=apsys.heartbeat.devel;Trusted_Connection=True;"
cd../../..
echo on
pause