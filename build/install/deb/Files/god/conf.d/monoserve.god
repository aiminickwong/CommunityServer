module God
  module Conditions
    class SocketConnectedWithinTimeout < SocketResponding
      def test
        socket = nil
        self.info = []
        begin
          timeout(5) do
            socket = UNIXSocket.new(self.path)
          end
        rescue Timeout::Error
          self.info = 'Socket connection timeout'
          return true
        rescue Exception => ex
          self.info = "Failed connected to socket with exception: #{ex}"
          socket.close if socket.is_a?(UNIXSocket)
          return true
        end
        socket.close
        self.info = 'Unix socket is responding'
        return false
      end
    end
  end
end

God.watch do |w|
  w.name = "monoserve"
  w.group = "onlyoffice"
  w.grace = 15.seconds
  w.start = "/etc/init.d/monoserve start"
  w.stop = "/bin/bash -c 'pgrep -f monoserve.log -U onlyoffice || true; sleep 4; pkill -SIGKILL -f monoserve.log -U onlyoffice || true'"
  w.pid_file = "/tmp/monoserve"
  w.unix_socket = "/var/run/onlyoffice/onlyoffice.socket"


  w.start_if do |start|
    start.condition(:process_running) do |c|
      c.interval = 10.seconds
      c.running = false
    end
  end

  w.restart_if do |restart|
    restart.condition(:socket_connected_within_timeout) do |c|
      c.family = 'unix'
      c.path = '/var/run/onlyoffice/onlyoffice.socket'
      c.times = 5
      c.interval = 10.seconds
    end
  end
end

God.watch do |w|
  w.name = "monoserve2"
  w.group = "onlyoffice"
  w.grace = 15.seconds
  w.start = "/etc/init.d/monoserve2 start"
  w.stop = "/bin/bash -c 'pgrep -f monoserve2.log -U onlyoffice || true; sleep 4; pkill -SIGKILL -f monoserve2.log -U onlyoffice || true'"
  w.pid_file = "/tmp/monoserve2"
  w.unix_socket = "/var/run/onlyoffice/onlyoffice2.socket"

  w.start_if do |start|
    start.condition(:process_running) do |c|
      c.interval = 10.seconds
      c.running = false
    end
  end

  w.restart_if do |restart|
    restart.condition(:socket_connected_within_timeout) do |c|
      c.family = 'unix'
      c.path = '/var/run/onlyoffice/onlyoffice2.socket'
      c.times = 5
      c.interval = 10.seconds
    end
  end
end