module Pitchspork
  class Review
    attr_reader :source_url

    def initialize(html, source_url)
      @doc = Nokogiri::HTML(html)
      @source_url = source_url
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

    def to_xml
      @xml ||= begin
        builder = Nokogiri::XML::Builder.new do |doc|
          doc.article {
            doc.source_url source_url
            doc.artist artist
            doc.album album
            doc.rating rating
            doc.label label
            doc.author author
            doc.body body
          }
        end
        builder.to_xml
      end
    end
  end
end
