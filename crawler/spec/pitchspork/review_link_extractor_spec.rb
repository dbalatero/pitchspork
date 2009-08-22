require File.dirname(__FILE__) + '/../spec_helper'

describe Pitchspork::ReviewLinkExtractor do
  before(:all) do
    html = raw_fixture('reviews_page_213.html')
    @extractor = Pitchspork::ReviewLinkExtractor.new(html)
  end

  describe "next_page" do
    it "should return the next page's URL if there is a next page" do
      @extractor.next_page.should == 'http://pitchfork.com/reviews/albums/214/'
    end

    it "should return nil if there is no next page" do
      extractor = Pitchspork::ReviewLinkExtractor.new(
        raw_fixture('reviews_page_213_no_next_page.html'))
      extractor.next_page.should be_nil
    end
  end

  describe "reviews" do
    it "should return a hash of article titles to links" do
      reviews = @extractor.reviews
      reviews['Barcelona: Simon Basic'].should == 'http://pitchfork.com/reviews/albums/559-simon-basic/'
    end
  end
end
