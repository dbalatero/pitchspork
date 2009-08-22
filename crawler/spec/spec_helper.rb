require 'spec'

$LOAD_PATH.unshift(File.dirname(__FILE__))
$LOAD_PATH.unshift(File.join(File.dirname(__FILE__), '..', 'lib'))
require 'pitchspork'

Spec::Runner.configure do |config|
end

def fixture_path(file)
  File.expand_path(File.dirname(__FILE__) + "/fixtures/" + file)
end

def raw_fixture(file)
  File.open(fixture_path(file), 'r') do |fp|
    fp.read
  end
end
