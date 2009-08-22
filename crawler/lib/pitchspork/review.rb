module Pitchspork
  class Review
    def initialize(html)
      @doc = Nokogiri::HTML(html)
    end

    def artist
      @artist ||= @doc.css('.title span.artists a b').first.content
    end

    def album
      @album ||= @doc.css('.title span.albums a').first.content
    end
    
    def rating
      @rating ||= @doc.css('div.large_rating').first.content.to_f
    end

    def label
      @label ||= @doc.css('h3 span.labels a').first.content
    end

    def body
      @body ||= begin
        text = @doc.css('div.content-container').last.content
        text.gsub!(/<\/?\w+>/, '')
        text.strip!
        text.gsub!(/[\r\n]+/, "\n\n")
        text
      end
    end

    def author
      @author ||= @doc.css('p.credits a').first.content
    end
  end
end
