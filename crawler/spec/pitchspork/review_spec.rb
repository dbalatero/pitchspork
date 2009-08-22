require File.dirname(__FILE__) + "/../spec_helper"

describe Pitchspork::Review do
  before(:all) do
    html = raw_fixture('squarepusher_solo_electric.html') 
    @review = Pitchspork::Review.new(html)
  end

  describe "artist" do
    it "should extract the artist name successfully" do
      @review.artist.should == 'Squarepusher'
    end
  end

  describe "album" do
    it "should extract the album name successfully" do
      @review.album.should == 'Solo Electric Bass 1'
    end
  end

  describe "rating" do
    it "should extract the rating successfully" do
      @review.rating.should == 5.6
    end
  end

  describe "label" do
    it "should extract the label successfully" do
      @review.label.should == 'Warp'
    end
  end

  describe "author" do
    it "should extract the author successfully" do
      @review.author.should == 'Patrick Sisson'
    end
  end

  describe "body" do
    it "should extract correctly without markup" do
      body = <<EOF
Getting called a virtuoso or mad scientist comes with some heavy baggage for a musician, so it says a lot about Tom Jenkinson, who records as Squarepusher, that he's been repeatedly labeled as both. Getting tagged with these contradictory stereotypes-- a classically trained workhorse mastering the canon versus an improvisational, intemperate tinkerer dismantling the rules-- goes a long way toward describing the Warp mainstay's musical output. He can sound technically brilliant and wickedly provocative at times, whether it's with restless, distended breakbeat patterns, buoyant bass-heavy fusion excursions, or airy combinations of these and other styles. And on his new album, in a gesture that could be a challenge in both senses of the word, he merely plugs in his bass and lets his playing speak for itself.

And bass, unaltered and without digital delay or effects, it definitely all you'll hear, whether it's delicate melodic progressions, arpeggiated chords or spitfire runs up the fretboard. While Jenkinson's skillful playing, the vital pulse of tracks like "Cooper's World" from Hard Normal Daddy or "Circlewave 2" from Hello Everything, has earned serious plaudits and a shout-out from Flea, it may be slightly surprisingly to hear how classically oriented some of the songs on Solo Electric Bass 1 are. Granted, he claims to have taught himself classical guitar at age 10, but the elegant, breakneck playing on tracks like "S.E.B. 6" resembles that of a Spanish guitarist, and "S.E.B. 5" is a rubbery, staccato splatter of notes that belies the thickness of bass strings. Hyperactive "S.E.B. 8" showcases Jenkinson's antsy side, bouncing between slap-happy antics and more slow and soulful passages. As the naming convention and out-of-order tracks suggest, the songs blend into each other. It creates an album weighed toward showcasing masterful execution that leaves a pretty muted general impression. Unless you're predisposed toward technical prowess and solo bass recordings, it's probably going to come off as more of a clinic than a collection of great songs.

In a 2006 XLR8R interview with Pitchfork contributor Mark Pytlik, Jenkinson spoke of avoiding the "music for musicians" tag. A limited-edition solo bass album recorded live at a Paris theater-- just one musician, one amp, and a six-string-- might appear pretty musician-friendly, leaning heavily toward virtuosic self-satisfaction. A large part of Jenkinson's fanbase isn't going to rush out to purchase uncut and unfiltered bass noodling, unless they were hoping for some intriguing digital manipulation or that the Loveless-esque cover was meant as a strong visual clue. But don't start with the simplistic slap bass/"Seinfeld" theme jokes without considering what these tracks say about Jenkinson's process and procedure. There's a reason the audience gets enthusiastic during this 12-song set. Jenkinson is technically one hell of a bass player. Perhaps by playing it straight, he's playing with people's definitions of what does and doesn't constitute musical skill in a digital world or merely showing off his own ridiculous abilities. For an artist known for restlessness and provocations, this may be a "mature" way to fuck with expectations.
EOF
      @review.body.should == body.strip
    end
  end
end
