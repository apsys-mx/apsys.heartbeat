echo off
cd bin/Debug/net6.0
apsys.heartbeat.migrationsexe /cnn:"Server=.;Database=apsys.heartbeat.devel;Trusted_Connection=True;"  /rollback:"one"
cd../../..
echo on
pause